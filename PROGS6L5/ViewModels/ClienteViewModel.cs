using System.ComponentModel.DataAnnotations;

namespace PROGS6L5.ViewModels
{
    public class ClienteViewModel
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }
    }
}