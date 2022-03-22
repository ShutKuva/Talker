using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Abstractions.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Core.Models.MiniModels;

namespace TalkerAPI.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICrudService<User> _crudUser;

        public UserController(ICrudService<User> crudUser)
        {
            _crudUser = crudUser;
        }

        [Authorize(policy: "try")]
        [HttpGet("read/{id}")]
        public async Task<ActionResult<User>> ReadUserById(int id)
        {
            var user = await _crudUser.ReadWithCondition(x => id == x.Id);

            if (user?.Any() ?? false)
            {
                return user.FirstOrDefault();
            }
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody]RegisterModel user)
        {
            User pseudoUser = new User(user);
            var status = await _crudUser.Create(pseudoUser, x => x.Username == user.Username);

            if (status)
            {
                return Ok();
            } else
            {
                Console.WriteLine(user.Surname);
                return BadRequest();
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
