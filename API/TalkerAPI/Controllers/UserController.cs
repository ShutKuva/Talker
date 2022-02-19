using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Abstractions.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;

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
        public async Task<IActionResult> CreateUser(User user)
        {
            var status = await _crudUser.Create(user, x => x.Username == user.Username);

            if (status)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
    }
}
