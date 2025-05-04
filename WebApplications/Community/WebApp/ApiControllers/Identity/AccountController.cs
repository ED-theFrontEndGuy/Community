using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Identity;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;
    private readonly Random _random = new Random();

    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, ILogger<AccountController> logger, SignInManager<AppUser> signInManager, AppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Login(
        [FromBody] LoginInfo loginInfo,
        // for testing token expiration
        [FromQuery] int jwtExpiresInSeconds,
        [FromQuery] int resfreshTokenExpiresInSeconds
    )
    {
        if (jwtExpiresInSeconds <= 0) jwtExpiresInSeconds = int.MaxValue;
        jwtExpiresInSeconds = jwtExpiresInSeconds < _configuration.GetValue<int>("JWTSecurity:ExpiresInSeconds")
            ? jwtExpiresInSeconds
            : _configuration.GetValue<int>("JWTSecurity:ExpiresInSeconds");
        
        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            // for security
            await Task.Delay(_random.Next(1000, 5000));
            return NotFound("User/Password problem");
        }
        
        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginInfo.Password,
                loginInfo.Email);
            await Task.Delay(_random.Next(1000, 5000));
            return NotFound("User/Password problem");
        }
        
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        if (!_context.Database.ProviderName!.Contains("InMemory"))
        {
            var deletedRows = await _context.RefreshTokens
                .Where(t => t.UserId == appUser.Id && t.Expiration < DateTime.UtcNow)
                .ExecuteDeleteAsync();
            _logger.LogInformation("Deleted {} refresh tokens", deletedRows);
        }
        else
        {
            //TODO: inMemory delete for testing
        }
        
        // TODO: set refresh token expiration
        var refreshToken = new AppRefreshToken()
        {
            UserId = appUser.Id
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWTSecurity:Key")!,
            _configuration.GetValue<string>("JWTSecurity:Issuer")!,
            _configuration.GetValue<string>("JWTSecurity:Audience")!,
            jwtExpiresInSeconds
        );
        
        var responseData = new JWTResponse()
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken
        };
        
        return Ok(responseData);
    }
}