using com.dyeingprinting.service.content.data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.dyeingprinting.service.content.data.Config
{
    public class CustomerCareConfig : IEntityTypeConfiguration<CustomerCare>
    {
        public void Configure(EntityTypeBuilder<CustomerCare> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(255);
            builder.Property(p => p.Description).HasMaxLength(255);
        }
    }
}
