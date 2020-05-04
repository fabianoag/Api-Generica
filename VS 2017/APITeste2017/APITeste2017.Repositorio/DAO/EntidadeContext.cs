using System;
using System.Collections.Generic;
using System.Text;
#region REFERÊNCIAS ADICIONADAS
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.rela;
using APITeste2017.Dominio;
#endregion

namespace APITeste2017.Repositorio.DAO
{
    public class EntidadeContext : DbContext
    {

        //Injecte da opções no context
        public EntidadeContext(DbContextOptions<EntidadeContext> options) : base(options) { }

        public DbSet<Usuario> usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Usuario>().HasKey(p => p.Id);       
        }
    }
}
