using System;
using System.Collections.Generic;
using System.Text;
#region REFERÊNCIAS ADICIONADAS
using System.Threading.Tasks;
using APITeste2017.Dominio;
#endregion

namespace APITeste2017.Repositorio.UsuarioDao
{
    public interface IUsuarioDao
    {
        Task<List<Usuario>> ListUser();
        Task<Usuario> Cadastrar(Usuario User);
        Task<Usuario> Login(Usuario User);
        Task<Usuario> PesquisaUsuarioLogin(Usuario login);
    }
}
