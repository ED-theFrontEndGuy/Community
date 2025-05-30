using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO;
using App.DTO.Identity;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Identity;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;
    private readonly Random _random = new Random();
    private const string UserPassProblem = "User/Password problem";
    private const int RandomDelayMin = 500;
    private const int RandomDelayMax = 5000;
    private const string SettingsJWTPrefix = "JWTSecurity:";
    private const string SettingsJWTKey = SettingsJWTPrefix + "Key";
    private const string SettingsJWTIssuer = SettingsJWTPrefix + "Issuer";
    private const string SettingsJWTAudience = SettingsJWTPrefix + "Audience";
    private const string SettingsJWTExpiresInSeconds = SettingsJWTPrefix + "ExpiresInSeconds";
    private const string SettingsJWTRefreshTokenExpiresInSeconds = SettingsJWTPrefix + "RefreshTokenExpiresInSeconds";


    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, ILogger<AccountController> logger, AppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
    }
    
    /// <summary>
    /// Register new user, returns JWT and refresh token
    /// </summary>
    /// <param name="registerModel">Reg info</param>
    /// <param name="jwtExpiresInSeconds">Optional custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional custom refresh token expiration</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Register(
        [FromBody]
        RegisterInfo registerModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds)
    {
        // check if user already registered
        var appUser = await _userManager.FindByEmailAsync(registerModel.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registerModel.Email);
            return BadRequest(new Message("User already registered"));
        }

        // register user
        var refreshToken = new AppRefreshToken()
        {
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
        };
        
        appUser = new AppUser()
        {
            Email = registerModel.Email,
            UserName = registerModel.Email,
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            AppRefreshTokens = new List<AppRefreshToken>() {refreshToken}
        };
        
        var result = await _userManager.CreateAsync(appUser, registerModel.Password);
        
        if (result.Succeeded)
        {
            await _userManager.AddClaimsAsync(appUser, new List<Claim>()
            {
                new(ClaimTypes.GivenName, appUser.FirstName),
                new(ClaimTypes.Surname, appUser.LastName)
            });
            
            _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

            var user = await _userManager.FindByEmailAsync(appUser.Email);
            if (user != null)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration.GetValue<string>(SettingsJWTKey)!,
                    _configuration.GetValue<string>(SettingsJWTIssuer)!,
                    _configuration.GetValue<string>(SettingsJWTAudience)!,
                    GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
                );
                _logger.LogInformation("WebApi login. User {User}", registerModel.Email);
                return Ok(new JWTResponse()
                {
                    JWT = jwt,
                    RefreshToken = refreshToken.RefreshToken,
                });
            }
            else
            {
                _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                return BadRequest(new Message("User not found after creation!"));
            }
        }

        var errors = result.Errors.Select(error => error.Description).ToList();
        return BadRequest(new Message() { Messages = errors });
    }
    
    /// <summary>
    /// User authentication, returns JWT and refresh token
    /// </summary>
    /// <param name="loginInfo">Login model</param>
    /// <param name="jwtExpiresInSeconds">Optional, use custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional, use custom refresh token expiration</param>
    /// <returns>JWT and refresh token</returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Login(
        [FromBody]
        LoginInfo loginInfo,
        // for testing token expiration
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            // for security
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new Message(UserPassProblem));
        }
        
        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning($"WebApi login failed, password {loginInfo.Password} for email {loginInfo.Email} was wrong");
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new Message(UserPassProblem));
        }
        
        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.GivenName, appUser.FirstName));
        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Surname, appUser.LastName));
        
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        if (!_context.Database.ProviderName!.Contains("InMemory"))
        {
            var deletedRows = await _context
                .RefreshTokens
                .Where(t => t.UserId == appUser.Id && t.Expiration < DateTime.UtcNow)
                .ExecuteDeleteAsync();
            _logger.LogInformation($"Deleted {deletedRows} refresh tokens");
        }
        else
        {
            //TODO: inMemory delete for testing
        }
        
        // set refresh token expiration
        var refreshToken = new AppRefreshToken()
        {
            UserId = appUser.Id,
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds)
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
        );
        
        var responseData = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken
        };
        
        return Ok(responseData);
    }
    
    /// <summary>
    /// Renew JWT using refresh token
    /// </summary>
    /// <param name="refreshTokenModel">Data for renewal</param>
    /// <param name="jwtExpiresInSeconds">Optional custom expiration for jwt</param>
    /// <param name="refreshTokenExpiresInSeconds">Optional custom expiration for refresh token</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> RenewRefreshToken(
        [FromBody]
        RefreshTokenModel refreshTokenModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        // extract jwt object, get user info from jwt
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.JWT);
            if (jwtToken == null)
            {
                return BadRequest(new Message("No token"));
            }
        }
        catch (Exception e)
        {
            return BadRequest(new Message($"Cant parse the token, {e.Message}"));
        }
        
        // validate jwt, ignore expiration date
        // https://stackoverflow.com/questions/49407749/jwt-token-validation-in-asp-net
        if (!IdentityExtensions.ValidateJwt(
                refreshTokenModel.JWT,
                _configuration.GetValue<string>(SettingsJWTKey)!,
                _configuration.GetValue<string>(SettingsJWTIssuer)!,
                _configuration.GetValue<string>(SettingsJWTAudience)!
            ))
        {
            return BadRequest(new Message("JWT validation failed"));
        }
        
        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        if (userEmail == null)
        {
            return BadRequest(new Message("No email in jwt"));
        }

        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound($"User with email {userEmail} not found");
        }
        
        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.GivenName, appUser.FirstName));
        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Surname, appUser.LastName));

        // load and compare refresh tokens
        await _context.Entry(appUser).Collection(u => u.AppRefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == refreshTokenModel.RefreshToken && x.Expiration > DateTime.UtcNow) ||
                (x.PreviousRefreshToken == refreshTokenModel.RefreshToken &&
                 x.PreviousExpiration > DateTime.UtcNow)
            )
            .ToListAsync();

        if (appUser.AppRefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }

        if (appUser.AppRefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }

        if (appUser.AppRefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found");
        }
        
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        // generate new jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>(SettingsJWTKey)!,
            _configuration.GetValue<string>(SettingsJWTIssuer)!,
            _configuration.GetValue<string>(SettingsJWTAudience)!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds)
        );

        // make new refresh token, obsolete old ones
        var refreshToken = appUser.AppRefreshTokens.First();
        if (refreshToken.RefreshToken == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshToken.RefreshToken;
            refreshToken.PreviousExpiration = DateTime.UtcNow.AddMinutes(1);

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJWTRefreshTokenExpiresInSeconds);

            await _context.SaveChangesAsync();
        }

        var res = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken,
        };

        return Ok(res);
    }
    
    private DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>(settingsKey)
            ? expiresInSeconds
            : _configuration.GetValue<int>(settingsKey);

        return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
    }
    
    /// <summary>
    /// Delete refresh token
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult> Logout(
        [FromBody]
        LogoutInfo logoutInfo
    )
    {
        // delete the refresh token - so user is kicked out after jwt expiration
        // We do not invalidate the jwt on serverside - that would require pipeline modification and checking against db on every request
        // so client can actually continue to use the jwt until it expires (keep the jwt expiration time short ~1 min)

        var appUser = await _context.Users
            .Where(u => u.Id == User.GetUserId())
            .SingleOrDefaultAsync();

        if (appUser == null)
        {
            return NotFound(new Message(UserPassProblem));
        }
        
        await _context.Entry(appUser)
            .Collection(u => u.AppRefreshTokens!)
            .Query()
            .Where(x =>
                x.RefreshToken == logoutInfo.RefreshToken || 
                x.PreviousRefreshToken == logoutInfo.RefreshToken
            )
            .ToListAsync();

        foreach (var appRefreshtoken in appUser.AppRefreshTokens!)
        {
            _context.RefreshTokens.Remove(appRefreshtoken);
        }
        
        var deleteCount = await _context.SaveChangesAsync();

        return Ok(new { TokenDeleteCount = deleteCount });
    }
}