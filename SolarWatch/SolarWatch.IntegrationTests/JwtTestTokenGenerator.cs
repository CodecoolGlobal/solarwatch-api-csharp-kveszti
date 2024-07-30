using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class JwtTestTokenGenerator
{
    private const int ExpirationMinutes = 30;

    public static string CreateTestToken(string userId, string userName, string email, string role)
    {
        var user = new IdentityUser
        {
            Id = userId,
            UserName = userName,
            Email = email
        };

        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user, role),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private static JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new JwtSecurityToken(
            issuer: "apiWithAuthBackend",
            audience: "apiWithAuthBackend",
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private static List<Claim> CreateClaims(IdentityUser user, string role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email)
        };

        if (!string.IsNullOrEmpty(role))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private static SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("SomethingVerySecretYouWillNeverFindOut")
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}
