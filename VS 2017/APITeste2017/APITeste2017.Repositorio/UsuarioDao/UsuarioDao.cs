using System;
using System.Collections.Generic;
using System.Text;
#region REFERÊNCIAS ADICIONADAS
using System.Threading.Tasks;
using APITeste2017.Dominio;
using APITeste2017.Repositorio.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
#endregion

namespace APITeste2017.Repositorio.UsuarioDao
{
    public class UsuarioDao : IUsuarioDao
    {
        private readonly EntidadeContext _context;

        public UsuarioDao(EntidadeContext context)
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
                _context.usuario.Add(User);
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
                var user = await _context.usuario.ToListAsync();
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
                var user = await _context.usuario.FirstOrDefaultAsync(c => c.Login.ToLower() == login.Login.ToLower());
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
                var user = await _context.usuario.ToListAsync();
                var resultado = user.Where(x => x.Login.ToLower() == usuario.Login.ToLower() && x.Senha == usuario.Senha).FirstOrDefault();
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
