using Agape316.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Agape316.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<EventItem> EventItem { get; set; }
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseSqlServer(@"Server=HODGES-PC\SQLEXPRESS;Database=Agape316;Trusted_Connection=True;MultipleActiveResultSets=true;");
#else
            // production connection string
            optionsBuilder.UseSqlServer(@"Server=174.138.185.50;Database=Agape316;User Id=dhodges351;password=Sbpkjabb@1;Trusted_Connection=False;MultipleActiveResultSets=true;");
            //optionsBuilder.UseLazyLoadingProxies();
#endif
        }
    }
}