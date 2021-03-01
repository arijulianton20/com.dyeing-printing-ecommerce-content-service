using com.dyeingprinting.service.content.data.Config;
using com.dyeingprinting.service.content.data.Model;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace com.dyeingprinting.service.content.data
{
    public class ContentDbContext : StandardDbContext
    {
        public ContentDbContext(DbContextOptions<ContentDbContext> options) : base(options)
        {

        }

        public DbSet<MobileContent> MobileContents { get; set; }
        public DbSet<WebContent> WebContents { get; set; }
        public DbSet<CustomerCare> CustomerCare { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MobileContentConfig());
            modelBuilder.ApplyConfiguration(new WebContentConfig());
            modelBuilder.ApplyConfiguration(new CustomerCareConfig());

            modelBuilder.Entity<MobileContent>().HasQueryFilter(entity => entity.IsDeleted == false);
            modelBuilder.Entity<WebContent>().HasQueryFilter(entity => entity.IsDeleted == false);
            modelBuilder.Entity<CustomerCare>().HasQueryFilter(entity => entity.IsDeleted == false);
        }
    }
}
