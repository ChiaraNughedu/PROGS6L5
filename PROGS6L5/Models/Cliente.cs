﻿using System.ComponentModel.DataAnnotations;
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

        
        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
