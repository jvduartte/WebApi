using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi_C_.Model;
using WebApi_C_.ORM;
using WebApi_C_.Repositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_C_.ORM;
using WebApi_C_.Repositorio;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_C_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _usuarioRepo;
        private readonly string _secretKey; // Chave secreta

        public UsuarioController(UsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _secretKey = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6"; // Deve ser armazenada em um local seguro
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] UsuarioDto usuarioDto)
        {
            // Verifica as credenciais
            var usuario = _usuarioRepo.GetByCredentials(usuarioDto.usuario, usuarioDto.senha);
            if (usuario == null)
            {
                return Unauthorized();
            }

            // Gera o token JWT
            var token = GenerateJwtToken(usuario);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(TbUsuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, usuario.Usuario), // Adaptar conforme o campo correto
            new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:7025"), // Definindo a audiência
            new Claim(JwtRegisteredClaimNames.Iss, "http://localhost:7025")  // Definindo o emissor
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}


