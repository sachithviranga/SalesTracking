using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesTracking.Entities.Auth;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Auth
{
    public class AuthHelper : IAuthHelper
    {
        private readonly TokenSettingsDTO _tokenSettings;
        public AuthHelper(IOptions<TokenSettingsDTO> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value;
        }

        public string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            bool validUser = false;
            try
            {
                validUser = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            }
            catch (Exception)
            {

            }
            return validUser;
        }

        public string GenerateToken(UserDTO user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            List<int> roleClaims = new();

            foreach (var userRole in user.UserRole)
            {
                foreach (var roleClaim in userRole.Role.RoleClaim.Where(a => a.IsActive == true))
                {
                    if (!roleClaims.Any(a => a == roleClaim.ClaimId))
                    {
                        roleClaims.Add(roleClaim.ClaimId);
                        claims.Add(new Claim("Permission", roleClaim.ClaimId.ToString()));
                    }
                }
            }

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes),
                signingCredentials: credentials,
                claims: claims
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
