using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APITeste.Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login {get; set;}
        public string Senha { get; set; }
    }
}
