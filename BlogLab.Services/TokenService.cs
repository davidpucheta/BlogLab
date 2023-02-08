using System.Security.Claims;
using System.Text;
using BlogLab.Models.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BlogLab.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly string _issuer;

    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty));
        _issuer = config["Jwt:Issuer"] ?? string.Empty;
    }

    public string CreateToken(ApplicationUserIdentity userIdentity)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, userIdentity.ApplicationUserId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userIdentity.Username),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience:_issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}