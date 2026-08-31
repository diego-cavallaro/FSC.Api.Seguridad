using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSC.Api.Seguridad.Modelos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FSC.Api.Seguridad.Services;
using BCrypt.Net;

namespace FSC.Api.Login.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        private readonly UserService _userService;

        public AutenticacionController(IConfiguration config, UserService userService)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
            _userService = userService;
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar(string loginName, string password)
        {
            var user = _userService.getByNickName(loginName);
            
            if(user != null && BCrypt.Net.BCrypt.Verify(password, user.password) && user.enable)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim("name", user.name));
                claims.AddClaim(new Claim("nickName", loginName));
                claims.AddClaim(new Claim("legajo", user.legajo));

                //Le damos una duración al token de 20 Hs
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(1200),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
        }
    }
}
