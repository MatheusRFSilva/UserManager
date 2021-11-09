using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data;
using UserManager.Models;
using UserManager.Repositories;
using UserManager.ViewModels;

namespace UserManager.Controllers
{
    [Route("v1/account")]
    public class UserController : Controller
    {

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser([FromServices] AppDbContext context)
        {
            var userRepository = new UserRepository(context);
            var users = await userRepository.GetUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUserById([FromServices] AppDbContext context,
        Guid id)
        {
            var userRepository = new UserRepository(context);
            var user = await userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("user")]

        public async Task<IActionResult> CreateUSer([FromServices] AppDbContext context,
        [FromBody] CreateUserViewModel model)
        {
            var userRepository = new UserRepository(context);
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Name = model.Name,
                Username = model.UserName,
                Password = model.Password
            };

            try
            {
                await userRepository.CreateUSer(user);

                return Created($"v1/account/user/{user.Id}", user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("user/{id}")]
        public async Task<IActionResult> UpdateUser([FromServices] AppDbContext context,
        [FromBody] UpdateUserViewModel model,
        Guid id)
        {
            var userRepository = new UserRepository(context);

            if (!ModelState.IsValid)
                return BadRequest();
            var user = await userRepository.GetUserById(id);

            if (user == null)
                return NotFound();

            try
            {
                await userRepository.UpdateUser(user, model);
                return Ok(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("user/{id}")]
        public async Task<IActionResult> DeleteUser([FromServices] AppDbContext context,
        Guid id)
        {
            var userRepository = new UserRepository(context);
            var user = await userRepository.GetUserById(id);

            if (user == null)
                return NotFound();

            try
            {
                await userRepository.DeleteUser(user);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}