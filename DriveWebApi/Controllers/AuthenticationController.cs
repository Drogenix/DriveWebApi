using DriverWebApi.Services.Tokens;
using DriveWebApi.Data;
using DriveWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _usersContext;

        private readonly ITokenService _tokenService;

        public AuthenticationController(ApplicationDbContext context, ITokenService tokenService)
        {
            _usersContext = context;

            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(AuthUser authUser)
        {
            var user = _usersContext.Users.FirstOrDefault(item => item.Login == authUser.Login && item.Password == authUser.Password);
            
            if (user != null)
            {
                var token = _tokenService.CreateToken(user.Login, user.Id);

                return Ok(token);
            }
            return Unauthorized("Пользователь не найден или введены некорректные данные.");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(AuthUser user)
        {
            var existsUser =  await _usersContext.Users.Where(item => item.Login == user.Login).ToListAsync();

            if(existsUser.Any())
            {
                return BadRequest("Этот логин занят!");
            }

            var newUser = new User() { Login = user.Login, Password = user.Password};

            _usersContext.Users.Add(newUser);

            await _usersContext.SaveChangesAsync();

            var addedUser = _usersContext.Users.Where(item => item.Login == user.Login).FirstOrDefault();

            if(addedUser != null)
            {
                var token = _tokenService.CreateToken(addedUser.Login, addedUser.Id);

                return Ok(token);
            }

            return BadRequest("Неизвестная ошибка. Попробуйте снова!");
        }

    }
}
