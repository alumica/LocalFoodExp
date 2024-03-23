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
    public class ReviewMap : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            //builder.HasOne(r => r.Traveler)
            //    .WithMany();

            //builder.HasOne(r => r.Dish)
            //    .WithMany();

            builder.Property(r => r.Rating)
                .HasDefaultValue(0); // 0 -> 5

            builder.Property(r => r.ReviewText)
                .HasMaxLength(2000);

            builder.Property(r => r.DateCreated)
                .HasColumnType("datetime");
        }
    }
}
