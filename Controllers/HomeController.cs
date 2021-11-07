using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Models;
using UserManager.Repositories;
using UserManager.Services;
using UserManager.ViewModels;

namespace UserManager.Controllers
{

    [Route("v1/account")]
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] AppDbContext context,
            [FromBody] User model)
        {
            var user = await context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username.ToLower() == model.Username.ToLower()
            && x.Password == model.Password);

            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado {User.Identity.Name}";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";


        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser([FromServices] AppDbContext context)
        {
            var users = await context
            .Users
            .AsNoTracking()
            .ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUserById([FromServices] AppDbContext context,
        Guid id)
        {
            var user = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("user")]

        public async Task<IActionResult> CreateUSer([FromServices] AppDbContext context,
        [FromBody] CreateUserViewModel model)
        {
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
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Created($"v1/account/user/{user.Id}", user);
            }
            catch (System.Exception)
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
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            try
            {
                user.Name = model.Name;
                user.Password = model.Password;
                user.Role = model.Role;

                await context
                .SaveChangesAsync();
                return Ok(user);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("user/{id}")]
        public async Task<IActionResult> DeleteUser([FromServices] AppDbContext context,
        Guid id)
        {
            var user = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();


            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
    }

}