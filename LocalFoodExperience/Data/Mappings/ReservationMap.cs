using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public class ReservationMap : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            //builder.Property(r => r.Date)
            //    .HasColumnType("date");

            //builder.Property(r => r.Time)
            //    .HasColumnType("time");

            //builder.Property(r => r.NumberOfGuests)
            //    .IsRequired()
            //    .HasDefaultValue(0);

            //builder.Property(r => r.Status) // 1: pending, 2: confirmed, 3: canceled, 4: completed
            //    .IsRequired()
            //    .HasDefaultValue(1);

            //builder.Property(r => r.SpecialRequests)
            //    .HasMaxLength(1000);

            //builder.Property(r => r.HostComments)
            //    .HasMaxLength(1000);

            //builder.HasOne(r => r.Traveler)
            //    .WithMany(u => u.Reservations)
            //    .HasForeignKey(r => r.TravelerId)
            //    .HasConstraintName("FK_Reservations_Travelers")
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(r => r.Host)
            //    .WithMany(u => u.Reservations)
            //    .HasForeignKey(r => r.HostId)
            //    .HasConstraintName("FK_Reservations_Hosts")
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
