using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PROGS6L5.Models;

namespace PROGS6L5.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>

    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public DateOnly Date { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
    }
}