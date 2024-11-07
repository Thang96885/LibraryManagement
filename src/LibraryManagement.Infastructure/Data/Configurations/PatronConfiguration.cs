using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Data.Configurations
{
    public class PatronConfiguration : IEntityTypeConfiguration<Patron>
    {
        public void Configure(EntityTypeBuilder<Patron> builder)
        {
            builder.ToTable("Patrons");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(x => x.Name)
                .HasMaxLength(40);
            builder.Property(x => x.Email)
                .HasMaxLength(100);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(10);
            builder.Property(x => x.Address)
                .HasConversion(address => address.ToString(),
                value => PatronAddress.Parse(value));
            ReservationIdConfig(builder);
            BorrowRecordIdConfig(builder);
            ReturnRecordIdConfig(builder);
        }

        private static void ReturnRecordIdConfig(EntityTypeBuilder<Patron> builder)
        {
            builder.OwnsMany(x => x.ReturnRecordIds, returnRecordIdBuilder =>
            {
                returnRecordIdBuilder.ToTable("PatronReturnRecordId");
                returnRecordIdBuilder.WithOwner().HasForeignKey("PatronId");
                returnRecordIdBuilder.Property(x => x.Value)
                    .HasColumnName("ReturnRecordId");
                returnRecordIdBuilder.HasKey("Value", "PatronId");
            });
            builder.Metadata.FindNavigation(nameof(Patron.ReturnRecordIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void BorrowRecordIdConfig(EntityTypeBuilder<Patron> builder)
        {
            builder.OwnsMany(x => x.BorrowRecordIds, borrowRecordIdBuilder =>
            {
                borrowRecordIdBuilder.ToTable("PatronBorrowRecordId");
                borrowRecordIdBuilder.WithOwner().HasForeignKey("PatronId");
                borrowRecordIdBuilder.Property(x => x.Value)
                    .HasColumnName("BorrowRecordId");
                borrowRecordIdBuilder.HasKey("Value", "PatronId");
            });
            builder.Metadata.FindNavigation(nameof(Patron.BorrowRecordIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ReservationIdConfig(EntityTypeBuilder<Patron> builder)
        {
            builder.OwnsMany(x => x.ReservationIds, reservationIdBuilder =>
            {
                reservationIdBuilder.ToTable("PatronReservationId");
                reservationIdBuilder.WithOwner().HasForeignKey("PatronId");
                reservationIdBuilder.Property(x => x.Value)
                .HasColumnName("ReservationId");
                reservationIdBuilder.HasKey("Value", "PatronId");
            });
            builder.Metadata.FindNavigation(nameof(Patron.ReservationIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
