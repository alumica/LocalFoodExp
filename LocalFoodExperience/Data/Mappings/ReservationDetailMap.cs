using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public class ReservationDetailMap : IEntityTypeConfiguration<ReservationDetail>
    {
        public void Configure(EntityTypeBuilder<ReservationDetail> builder)
        {
            builder.ToTable("ReservationDetails");

            //builder.HasOne(r => r.Reservation)
            //    .WithMany(d => d.ReservationDetails)
            //    .HasForeignKey(r => r.ReservationId)
            //    .HasConstraintName("FK_ReservationDetails_Reservations")
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(r => r.Dish)
            //    .WithMany(d => d.ReservationDetails)
            //    .HasForeignKey(r => r.DishId)
            //    .HasConstraintName("FK_ReservationDetails_Dishes")
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(r => new { r.ReservationId, r.DishId });


            builder.Property(r => r.NumberOfDishes)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(r => r.Price)
                .IsRequired()
                .HasPrecision(12, 10);

            builder.Property(r => r.Note)
                .HasMaxLength(1000);
        }
    }
}
