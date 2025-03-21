using System.ComponentModel.DataAnnotations;
using PROGS6L5.Models;

namespace PROGS6L5.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

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

        // Relazione con Prenotazione (1 cliente può avere molte prenotazioni)
        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
