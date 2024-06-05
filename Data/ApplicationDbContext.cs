using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ski.Domain.Entities;

namespace Apolchevskaya.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Skii> Skii { get; set; } = default!;

        public DbSet<Category> Categories { get; set; }

        
    }
}
