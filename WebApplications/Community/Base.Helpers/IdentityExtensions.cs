using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdStr = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userIdStr);
        
        return userId;
    }
    
    private readonly static JwtSecurityTokenHandler JWTSecurityTokenHandler = new JwtSecurityTokenHandler();
        
    public static string GenerateJwt(
        IEnumerable<Claim> claims,
        string key,
        string issuer,
        string audience,
        DateTime expires)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );
        
        return JWTSecurityTokenHandler.WriteToken(token);
    }
}