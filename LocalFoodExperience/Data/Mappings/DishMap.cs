using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public class DishMap : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(d => d.Description)
                .HasMaxLength(1000);

            builder.Property(d => d.DietaryRestrictions)
                .HasMaxLength(1000);

            builder.Property(d => d.Price)
                .IsRequired()
                .HasPrecision(12, 10);

            builder.Property(d => d.Currency)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.Photos)
                .HasMaxLength(2000);

            //builder.HasOne(d => d.Host)
            //    .WithMany(u => u.Dishes)
            //    .HasForeignKey(d => d.HostId)
            //    .HasConstraintName("FK_Dishes_Users")
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(d => d.Category)
            //    .WithMany(u => u.Dishes)
            //    .HasForeignKey(d => d.CategoryId)
            //    .HasConstraintName("FK_Dishes_Categories")
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
