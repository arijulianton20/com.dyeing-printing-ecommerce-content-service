using com.dyeingprinting.service.content.data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.dyeingprinting.service.content.data.Config
{
    public class WebContentConfig : IEntityTypeConfiguration<WebContent>
    {
        public void Configure(EntityTypeBuilder<WebContent> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Title).HasMaxLength(255);

        }
    }
}
