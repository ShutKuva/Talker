using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using Core.Models.MiniModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TalkerAPI.Models;

namespace TalkerAPI.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        public ICrudService<User> _userCrudService;
        public JwtParams _jwtParams;

        public AuthenticationController(ICrudService<User> userCrudService, IOptions<JwtParams> jwtParams)
        {
            _userCrudService = userCrudService;
            _jwtParams = jwtParams?.Value ?? throw new ArgumentNullException(nameof(jwtParams));
        }

        [AllowAnonymous]
        [Route("auth")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
        {
            var allUsers = await _userCrudService.ReadWithCondition(x => x.Username == model.Username && x.Password == model.Password);
            if (allUsers?.FirstOrDefault() != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("username", model.Username)
                };
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _jwtParams.Issuer,
                    audience: _jwtParams.Audience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtParams.Key)), SecurityAlgorithms.HmacSha256)
                    );
                string jwtoken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    token = jwtoken
                });
            }
            return BadRequest();
        }
    }
}
