
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebMigrationsBack.Interfaces;
using WebMigrationsBack.Models;

namespace WebMigrationsBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJWTokenService _jwtTokenSrv;
        public AuthController(IJWTokenService wtTokenSrv)
        {
            _jwtTokenSrv = wtTokenSrv;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel login)
        { 
            string email = login.email;
            try
            {
                if (login != null)
                {
                    string fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "Models", "UsersDummies.json");

                    if (!System.IO.File.Exists(fileLocation))
                    {
                        throw new FileNotFoundException("No se encontró el archivo Json");
                    }

                    string jsonString = System.IO.File.ReadAllText(fileLocation);
                    List<UserModel> usuarios = JsonSerializer.Deserialize<List<UserModel>>(jsonString);
                    UserModel? user = usuarios.FirstOrDefault(u => u.email != null && u.email.Equals(email, StringComparison.OrdinalIgnoreCase));

                    if(user != null)
                    {
                        var token = await _jwtTokenSrv.GenerateJwtToken(user.email);
                        user.token = token;

                        return Ok(new { user = user });
                    }
                    return NotFound();
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }   
}