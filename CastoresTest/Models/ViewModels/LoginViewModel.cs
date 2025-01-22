using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CastoresTest.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress(ErrorMessage = "Es necesario usar un {0} valido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La {0} es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool Rememberme { get; set; }
    }
}
