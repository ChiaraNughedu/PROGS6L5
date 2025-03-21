using System.ComponentModel.DataAnnotations;
using PROGS6L5.Models;

namespace PROGS6L5.Models
{
    public class Camera
    {
        [Key]
        public int CameraId { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public decimal Prezzo { get; set; }

        // Relazione con Prenotazione (1 camera può avere molte prenotazioni)
        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
