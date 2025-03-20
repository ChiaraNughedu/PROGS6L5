using Microsoft.AspNetCore.Identity;
using PROGS6L5.Models;

namespace PROGS6L5.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

    }
}
