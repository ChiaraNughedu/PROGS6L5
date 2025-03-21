using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PROGS6L5.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "La password deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "Le password non corrispondono.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Ruolo")]
        public string Ruolo { get; set; } // "Admin" o "Staff"
    }
}
