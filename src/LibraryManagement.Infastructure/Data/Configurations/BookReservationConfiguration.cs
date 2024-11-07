using LibraryManagement.Domain.BookReservationAggregate;
using LibraryManagement.Domain.BookReservationAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Data.Configurations
{
    public class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
    {
        public void Configure(EntityTypeBuilder<BookReservation> builder)
        {
            builder.HasKey(br => br.Id);
            builder.Property(br => br.Id)
                .ValueGeneratedOnAdd();

            builder.Property(br => br.ReservationDate)
                .IsRequired();

            builder.Property(br => br.ReservationExpirationDate);

            builder.Property(br => br.IsReserved)
                .IsRequired();

            builder.Property(br => br.PatronId)
                .HasConversion(id => id.Value, value => BookReservationPatronId.Create(value));

            builder.OwnsMany(br => br.BookId, bookBuilder =>
            {
                builder.ToTable("ReservationBookIds");
                bookBuilder.WithOwner().HasForeignKey("BookReservationId");
                bookBuilder.Property(b => b.Value).HasColumnName("BookId");
                bookBuilder.HasKey("Value", "BookReservationId");
            });
        }
    }
}
