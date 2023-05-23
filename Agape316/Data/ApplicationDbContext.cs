using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agape316.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CategoryDescription> CategoryDescriptions { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventDish> EventDishes { get; set; }

        public virtual DbSet<EventSlot> EventSlots { get; set; }
    }
}
