using LibraryManagement.Domain.BookReservationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Configurations
{
	public class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
	{
		public void Configure(EntityTypeBuilder<BookReservation> builder)
		{
			builder.HasKey(br => br.Id);

			builder.Property(br => br.ReservationDate)
				.IsRequired();

			builder.Property(br => br.ReservationExpirationDate);

			builder.Property(br => br.IsReserved)
				.IsRequired();

			builder.OwnsOne(br => br.PatronId, patronBuilder =>
			{
				patronBuilder.WithOwner();
			});

			builder.OwnsMany(br => br.BookId, bookBuilder =>
			{
				bookBuilder.WithOwner().HasForeignKey("BookReservationId");
				bookBuilder.Property(b => b.Value).HasColumnName("BookId");
				bookBuilder.HasKey("Value", "BookReservationId");
			});
			builder.Metadata.FindNavigation(nameof(BookReservation.BookId))!
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
