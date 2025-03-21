using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PROGS6L5.Models;

namespace PROGS6L5.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public DateOnly? BirthDate { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        
    }
}
