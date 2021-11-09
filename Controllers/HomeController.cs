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



    }

}