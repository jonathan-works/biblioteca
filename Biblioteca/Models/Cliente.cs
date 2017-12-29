using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using Biblioteca.Helpers;

namespace Biblioteca.Models
{
    public class Cliente
    {
        public virtual int id { get; set; }
        [Required]
        [DisplayName("Nome Cliente")]
        public virtual string nome { get; set; }

        [Required]
        [DisplayName("CPF")]  
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public virtual string cpf { get; set; }
        [Required]
        [DisplayName("E-mail")]
        public virtual string email { get; set; }
    }           
}