using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TalkerAPI.Models;

namespace TalkerAPI.Authorization
{
    public class JwtAuthenticateMiddleware
    {
        public RequestDelegate _next { get; set; }
        public JwtParams _jwtParams { get; set; }

        public JwtAuthenticateMiddleware(RequestDelegate next, IOptions<JwtParams> jwtParameters)
        {
            _next = next;
            _jwtParams = jwtParameters.Value ?? throw new ArgumentNullException(nameof(jwtParameters));
        }

        public Task Invoke(HttpContext context)
        {
            try
            {
                string token = context.Request.Headers["Authorization"];
                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
                if(token != null)
                    context.SignInAsync(jwtHandler.ValidateToken(token, _jwtParams.GenerateTokenValidationParameters(), out SecurityToken preJwt));
            } catch (Exception ex)
            { 
                return Task.FromException(ex);
            }
            return Task.CompletedTask;
        }
    }
}
