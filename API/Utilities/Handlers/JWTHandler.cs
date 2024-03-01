using API.Utilities.Handlers.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handlers
{
    public class JWTHandler : IJWTHandler
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiration;

        public JWTHandler( string key, string issuer, string audience, int expiration)
        {
            _expiration = expiration;
            _key = key;
            _issuer = issuer;
            _audience = audience;
        }

        public string Generate(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(issuer: _issuer,
                                                    audience: _audience,
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(_expiration),
                                                    signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
