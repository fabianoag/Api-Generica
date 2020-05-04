using APITeste.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITeste.Repositorio.DAO
{
    public class EntidadeContext: DbContext
    {
        //Injecte da opções no context
        public EntidadeContext(DbContextOptions<EntidadeContext> options) : base(options) { }

        public DbSet<Usuario> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Usuario>().HasKey(p => p.Id);
            builder.Entity<Usuario>().ToTable("usuario");
        }
    }
}
