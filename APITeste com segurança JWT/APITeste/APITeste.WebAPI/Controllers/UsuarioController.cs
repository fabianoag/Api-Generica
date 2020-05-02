using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITeste.Dominio;
using APITeste.Repositorio.UsuarioDAO;
using Microsoft.AspNetCore.Mvc;
using APITeste.WebAPI.Helper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
#region REFERÊNCIAS ADICIONAS
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using APITeste.WebAPI.DTO.UsuarioDTO;
using AutoMapper;
#endregion

namespace APITeste.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioDAO _user;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioDAO user, IConfiguration config, IMapper mapper)
        {
            _user = user;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("cadastro")]
        [AllowAnonymous]
        public async Task<IActionResult> cadastro(UsuarioDto usuario)
        {            
            var _usuario = _mapper.Map<Usuario>(usuario);
            var users = await _user.PesquisaUsuarioLogin(_usuario);
            if (users != null)
            {
                var _messageStatus = new messageStatus();
                _messageStatus.Status = "Unauthorized";
                _messageStatus.Message = "Login já esta sendo usado.";
                return Unauthorized(_messageStatus);
            }
                
            var i = await _user.Cadastrar(_usuario);
            return Ok("O id é igual a: " + i.Id.ToString());
        }

        [HttpGet("users")]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _user.ListUser();
            return Ok(users);
        }

        [HttpGet("entity")]
        [AllowAnonymous]
        public async Task<IActionResult> Entity()
        {
            var usuarioDto = new UsuarioDto();    
            return Ok( usuarioDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario user)
        {
            var _messageStatus = new messageStatus();
            var usuario = await _user.Login(user);
            if (usuario == null)
            {
                _messageStatus.Status = "Unauthorized";
                _messageStatus.Message = "Login ou senha incorreta.";
                return Unauthorized(_messageStatus);
            }
            else
            {
                return Ok(new
                {
                    token = GenerateJWTToken(usuario).Result,
                    user = usuario
                });
            };
        }

        /// <summary>
        /// Método usado para gerar um token de usuário.
        /// </summary>
        /// <param name="user">Recebe um usuário</param>
        /// <returns>Retorna um token.</returns>
        private async Task<string> GenerateJWTToken(Usuario user)
        {
            try
            {
                /* Criando uma lista de claim para adiciona no token.*/
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login.ToString())
                };

                #region SE ACASO PRECISA
                // Pesquisar as roles do usuários.
                //List<string> Roles = new List<string>();
                //Roles.Add("ADM");

                // Listar roles.
                //foreach (var role in Roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, role));
                //}
                #endregion

                //Pegar a chave em "AppSettings:Token".
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                    _config.GetSection("AppSettings:TokenKey").Value));


                //Cria um credencial com a chave.
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                //Opcões de configuração do token criado.
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(0.1255),
                    SigningCredentials = creds
                };

                // Instância do "JwtSecurityTokenHandler()".
                var tokenHandler = new JwtSecurityTokenHandler();

                //Cria o token.
                var token = tokenHandler.CreateToken(tokenDescription);

                //Retorna o token escrito.
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}