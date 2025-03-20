using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PROGS6L5.Models;

namespace PROGS6L5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
            IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.ApplicationUserRoles)
                .HasForeignKey(ur => ur.UserId);

            // Configura la relazione tra ApplicationUserRole e ApplicationRole
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.ApplicationUserRoles)
                .HasForeignKey(ur => ur.RoleId);

            //configurare la relazione tra le tabelle ApplicationUser con la tabella(modello) dei prodotti o altro a seconda dell'esercizio
        }
    }

}

