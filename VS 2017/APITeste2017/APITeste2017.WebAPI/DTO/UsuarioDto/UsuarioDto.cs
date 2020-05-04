using System.ComponentModel.DataAnnotations;

namespace APITeste2017.WebAPI.DTO.UsuarioDto
{
    public class UsuarioDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo login e obrigatório.")]
        [RegularExpression("^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Ñão pode ter caracteres especiais, sem espaço e acento.")]
        public string Login { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo senha e obrigatório.")]
        [StringLength(11, ErrorMessage = "Senha tem que ser maior que 3, maximo 11 caracteres.", MinimumLength = 4)]
        [RegularExpression("^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Ñão pode ter caracteres especiais, sem espaço e acento.")]
        public string Senha { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo confirme senha e obrigatório.")]
        [Compare("Senha", ErrorMessage = "As senhas não são iguais.")]
        [StringLength(11, ErrorMessage = "Senha tem que ser maior que 3, maximo 11 caracteres.", MinimumLength = 4)]
        public string ConfirmeSenha { get; set; }
    }
}
