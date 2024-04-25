using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AuthCore.Helpers;

namespace AuthCore.Models
{
    [Table("SegurancaUsuario")]
    public class SegurancaUsuarioModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string nome { get; set; }

        [Required]
        [StringLength(11)]
        public string cpf { get; set; }

        [Required]
        [StringLength(150)]
        public string email { get; set; }

        [Required]
        [StringLength(250)]
        public string senha { get; set; }

        public DateTime? dtaCriacao { get; set; }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            senha = EncryptPassword.Encode(novaSenha);
            return novaSenha;
        }
    }
}
