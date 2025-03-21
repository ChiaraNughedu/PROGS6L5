using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROGS6L5.Models;

namespace PROGS6L5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
            IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Identity tables (opzionali, se vuoi usarli in modo esplicito)
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        // Entità del progetto per la gestione delle prenotazioni dell'hotel
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione per Identity: relazioni tra ApplicationUser, ApplicationRole e ApplicationUserRole
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.ApplicationUserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.ApplicationUserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Configurazione relazione Cliente - Prenotazione (1:N)
            modelBuilder.Entity<Prenotazione>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Prenotazioni)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurazione relazione Camera - Prenotazione (1:N)
            modelBuilder.Entity<Prenotazione>()
                .HasOne(p => p.Camera)
                .WithMany(c => c.Prenotazioni)
                .HasForeignKey(p => p.CameraId)
                .OnDelete(DeleteBehavior.Cascade);

            // Eventuali ulteriori configurazioni (ad esempio, per seeding o regole specifiche)
        }
    }
}
