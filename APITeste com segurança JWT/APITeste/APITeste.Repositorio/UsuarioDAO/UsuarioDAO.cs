using APITeste.Dominio;
using APITeste.Repositorio.DAO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APITeste.Repositorio.UsuarioDAO
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly EntidadeContext _context;
        public UsuarioDAO(EntidadeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="User">Recebe o usuario</param>
        /// <returns>Retorna int.</returns>
        public async Task<Usuario> Cadastrar(Usuario User)
        {
            try
            {
                _context.usuarios.Add(User);
                await _context.SaveChangesAsync();                
                return User;

            }
            catch (Exception ex)
            {
                //Dispara uma messagem de erro.
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Usuario>> ListUser()
        {
            try
            {
                var user = await _context.usuarios.ToListAsync();
                return user;

            }
            catch (Exception ex)
            {
                //Dispara uma messagem de erro.
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario> PesquisaUsuarioLogin(Usuario login)
        {
            try
            {
                var user = await _context.usuarios.FirstOrDefaultAsync(c => c.Login.ToLower() == login.Login.ToLower());
                return user;

            }
            catch (Exception ex)
            {
                //Dispara uma messagem de erro.
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            try
            {
                var user = await _context.usuarios.ToListAsync();
                var resultado = user.Where(x => x.Login == usuario.Login.ToLower() && x.Senha.ToLower() == usuario.Senha.ToLower()).FirstOrDefault();
                return resultado;

            }
            catch (Exception ex)
            {
                //Dispara uma messagem de erro.

                throw new Exception(ex.Message);
            }
        }

        public string MsgErro(string erro)
        {
            switch (erro)
            {
                case null:
                    return "Login ou senha incorreta.";
                default:
                    return erro;                 
            }

            
        }
    }
}
