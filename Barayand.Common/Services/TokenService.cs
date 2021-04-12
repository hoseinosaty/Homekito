using Barayand.OutModels.Miscellaneous;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Barayand.Common.Services
{
    public class TokenService
    {
        private static string Secret = "QmFyYXluZENwYW5lbFN5c3RlbURlc2lnbmVkQnlIb3NlaW5Pc2F0aS5pbjEzLTEtMjAyMC1QaG9uZU51bWJlcjA5MTgwNTI0OTYxRW1haWxaY3BjLmNvQGdtYWlsLmNvbQ==";
        public static string GenerateToken(int id,string username,string email)
        {
            IdentityKey identityKey = new IdentityKey();
            identityKey.Email = email;
            identityKey.UserName = username;
            identityKey.Id = id;
            identityKey.ExpiresLogin = DateTime.Now.AddHours(12);
            string Token = Barayand.Common.Services.CryptoJsService.EncryptStringToAES(JsonConvert.SerializeObject(identityKey));
            return Token;
            //byte[] key = Convert.FromBase64String(Secret);
            //SymmetricSecurityKey securityKey;
            //securityKey = new SymmetricSecurityKey(key);
            //SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[] {
            //    new Claim(ClaimTypes.Name, username)
            //}),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            //};
            //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            //return handler.WriteToken(token);
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
        public static Cookie SetUserCookie(string token)
        {
            
            Cookie UserLogin = new Cookie();
            UserLogin.Name = "UserAuthentication";
            UserLogin.Expires = DateTime.UtcNow.AddHours(1);
            UserLogin.Value = token;
            return UserLogin;
        }
        public static int AuthorizeUser(Microsoft.AspNetCore.Http.HttpRequest Request)
        {
            if (Request.Cookies["HomeKitoUser"] != null)
            {
                string cookie = Request.Cookies["HomeKitoUser"];
                var loginInfo = Barayand.Common.Services.CryptoJsService.DecryptStringAES(cookie);
                IdentityKey identityKey = JsonConvert.DeserializeObject<IdentityKey>(loginInfo);
                if (DateTime.Now > identityKey.ExpiresLogin)
                {
                    return 0;
                }
                return identityKey.Id;
            }
            else
            {
                return -1;
            }
        }
    }
}
