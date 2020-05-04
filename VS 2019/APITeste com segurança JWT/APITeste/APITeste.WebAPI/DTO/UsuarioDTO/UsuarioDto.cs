using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITeste.WebAPI.DTO.UsuarioDTO
{
    // Se no caso for preciso cadastrar ou recupera usuário.
    public class UsuarioDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "O campo login e obrigatório.")]
        [RegularExpression("^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Ñão pode ter caracteres especiais, sem espaço e acento.")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo senha e obrigatório.")]
        [StringLength(11,ErrorMessage = "Senha tem que ser maior que 3, maximo 11 caracteres.", MinimumLength = 4)]
        [RegularExpression("^[a-zA-Z0-9_]{5,20}$", ErrorMessage ="Ñão pode ter caracteres especiais, sem espaço e acento.")]
        public string Senha { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo confirme senha e obrigatório.")]
        [Compare("Senha",ErrorMessage = "As senhas não são iguais.")]
        [StringLength(11, ErrorMessage = "Senha tem que ser maior que 3, maximo 11 caracteres.", MinimumLength = 4)]
        public string ConfirmeSenha { get; set; }
    }
}
