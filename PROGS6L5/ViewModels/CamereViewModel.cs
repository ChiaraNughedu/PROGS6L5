using System.ComponentModel.DataAnnotations;

namespace PROGS6L5.ViewModels
{
    public class CamereViewModel
    {
        [Required]
        [StringLength(50)]
        public string Numero { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        public decimal Prezzo { get; set; }

       
    }
}