using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtServices
{
    public class TokenManager
    {
        // The secret can be any random text. This is a base64 text created from a random phrase.
        private static string Secret = "TW9yYWlscyBXZWIzIEFQSXMgYXJlIHRoZSBncmVhdGVzdCB0aGluZyBzaW5jZSBzbGljZWQgYnJlYWQh";

        /// <summary>
        /// Generates a new JWT Token with the specified claims.
        /// Claims are pieces of information such as ID Tokens, user name, etc.
        /// </summary>
        /// <param name="appClaims">IDictionary<string, string></param>
        /// <returns>string</returns>
        public static string GenerateToken(IDictionary<string, string> appClaims)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            List<Claim> claims = new List<Claim>();

            // Capture each of the custom claims that will be included
            // in the JWT.
            if (appClaims != null && appClaims.Count > 0)
            {
                foreach (string k in appClaims.Keys)
                {
                    claims.Add(new Claim(k, appClaims[k]));
                }
            }

            // Define the token data.
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }

        /// <summary>
        /// Reads a JWT token and extracts the Claims into a ClaimsPrincipal.
        /// </summary>
        /// <param name="token">string</param>
        /// <returns>ClaimsPrincipal</returns>
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                // Read the JSON string into a Jwt object.
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

                if (jwtToken == null)
                {
                    return null;
                }

                // Use our know secret
                byte[] key = Convert.FromBase64String(Secret);

                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken securityToken;

                // Validate the token using the known secret and extract any claims.
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Validates a JWT.
        /// </summary>
        /// <param name="token">string</param>
        /// <returns>bool</returns>
        public static bool ValidateToken(string token)
        {
            bool resp = false;

            ClaimsPrincipal principal = GetPrincipal(token);

            if (principal != null && principal.Identity != null)
            {
                try
                {
                    ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;


                    Claim? firstClaim = identity.FindFirst(ClaimTypes.Name);

                    resp = firstClaim?.Value != null;
                }
                catch (NullReferenceException)
                {
                    // Do nothing resp will be false.
                }
            }

            return resp;
        }
    }
}