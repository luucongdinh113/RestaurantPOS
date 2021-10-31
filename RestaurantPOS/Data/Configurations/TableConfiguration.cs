using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantPOS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPOS.Data.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("TABLE");
            builder.HasKey(table => table.Id);
            builder.Property(table => table.Name).IsRequired().IsUnicode().HasMaxLength(30);
            builder.Property(table => table.PeopleCount).IsRequired();
        }
    }
}
