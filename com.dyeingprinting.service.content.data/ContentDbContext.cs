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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MobileContentConfig());
            modelBuilder.ApplyConfiguration(new WebContentConfig());
        }
    }
}
