using APITeste.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APITeste.Repositorio.UsuarioDAO
{
    public interface IUsuarioDAO
    {
        Task<List<Usuario>> ListUser();
        Task<Usuario> Cadastrar(Usuario User);
        Task<Usuario> Login(Usuario User);
        Task<Usuario> PesquisaUsuarioLogin(Usuario login);
    }
}
