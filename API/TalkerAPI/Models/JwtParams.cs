using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TalkerAPI.Models
{
    public class JwtParams
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }

        public TokenValidationParameters GenerateTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
                ValidateIssuerSigningKey = true,
            };
        }
    }
}
