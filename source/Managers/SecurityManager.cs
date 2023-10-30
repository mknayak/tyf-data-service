using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tyf.data.service.Extensions;
using tyf.data.service.Models;

namespace tyf.data.service.Managers
{
    public class SecurityManager : ISecurityManager
    {
        private readonly SecurityOption securityOption;

        public SecurityManager(IOptions<SecurityOption> options)
        {
            this.securityOption = options.Value;
        }
        //	public static string SecurityKey = "1e7a3d9d4fa51b7f24107366e08363f19e74dbfafd8e9d368058ab53279b6d28be19b93de2a5e17617279ebcf3c38181efc4f7c138d19232683bfe276cce3782";//Read from configuration
        public string ComputeHash(string salt, string strToHash)
        {
            var input = $"{salt}{strToHash}";
            var hasedBytes = MD5.HashData(System.Text.Encoding.UTF8.GetBytes(input));
            return System.Convert.ToBase64String(hasedBytes);
        }
        public bool Compare(string salt, string strToHash, string hashedValue)
        {
            var hash = ComputeHash(salt, strToHash);
            return string.Equals(hash, hashedValue);
        }
        public AuthToken CreateToken(UserModel model)
        {
            var claims = new List<Claim>();
            if (null != model.Roles && model.Roles.Any())
            {
                claims.AddRange(model.Roles.Select(x => new Claim(ClaimTypes.Role, x)));
            }
            claims.Add(new Claim(ClaimTypes.Email, model.Email));
            claims.Add(new Claim(ClaimTypes.GivenName, model.Name));
            if (null != model.Namespaces && model.Namespaces.Any())
            {
                claims.AddRange(model.Namespaces.Select(x => new Claim("scope", x)));
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityOption.SignKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                                   claims: claims,
                                   expires: DateTime.UtcNow.AddDays(1),
                                   signingCredentials: cred
                                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthToken { Token = jwt };
        }
        public UserModel? ValidateToken(AuthToken authToken)
        {
            if (authToken?.Token == null) return null;
            try
            {
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityOption.SignKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenValidationParams = new TokenValidationParameters() { IssuerSigningKey = key ,
                      ValidateAudience= false,
                      ValidateIssuer=false
                      
                };
                var claims = new JwtSecurityTokenHandler().ValidateToken(authToken.Token, tokenValidationParams, out var validatedToken);
                var model = new UserModel()
                {
                    Name = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
                    Email = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                };
                model.Roles = claims.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return model;
                 
            }
            catch(Exception ex)
            {
                return null ;
            }
        }

    }
}

