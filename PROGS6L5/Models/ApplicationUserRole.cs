using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PROGS6L5.Models;

namespace PROGS6L5.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>

    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public DateOnly Date { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
    }
}