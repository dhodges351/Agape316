﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agape316.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}       
        
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventDish> EventDish { get; set; }
        public virtual DbSet<MealSchedule> MealSchedule { get; set; }
        public virtual DbSet<MealDelivery> MealDelivery { get; set; }
        public virtual DbSet<AgapeDocument> AgapeDocument { get; set; }
    }
}
