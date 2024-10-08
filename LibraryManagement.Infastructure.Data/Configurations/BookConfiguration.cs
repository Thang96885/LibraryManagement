using LibraryManagement.Domain.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable("Books");

			builder.HasKey(b => b.Id);

			builder.Property(b => b.Title)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(b => b.AuthorId)
				.IsRequired(false);

			builder.Property(b => b.PublisherName)
				.HasMaxLength(200)
				.IsRequired();

			// Cấu hình cho các collection
			ConfigGenre(builder);
			ConfigReservation(builder);
			ConfigBorrowRecord(builder);
			ConfigReturnRecord(builder);
		}

		private static void ConfigBorrowRecord(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.BorrowRecordIds, borrowRecordBuilder =>
			{
				borrowRecordBuilder.WithOwner().HasForeignKey("BookId");
				borrowRecordBuilder.Property<Guid>("Id");
				borrowRecordBuilder.HasKey("Id");
				borrowRecordBuilder.ToTable("BorrowRecords");
			});

			builder.Metadata.FindNavigation(nameof(Book.BorrowRecordIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private static void ConfigReservation(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.BookReservationId, bookReservationBuilder =>
			{
				bookReservationBuilder.WithOwner().HasForeignKey("BookId");
				bookReservationBuilder.Property<Guid>("Id");
				bookReservationBuilder.HasKey("Id");
				bookReservationBuilder.ToTable("BookReservations");
			});

			builder.Metadata.FindNavigation(nameof(Book.BookReservationId)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private static void ConfigReturnRecord(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.ReturnRecordIds, returnRecordBuilder =>
			{
				returnRecordBuilder.WithOwner().HasForeignKey("BookId");
				returnRecordBuilder.Property<Guid>("Id");
				returnRecordBuilder.HasKey("Id");
				returnRecordBuilder.ToTable("ReturnRecords");
			});

			builder.Metadata.FindNavigation(nameof(Book.ReturnRecordIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private static void ConfigGenre(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.GenreIds, genreBuilder =>
			{
				genreBuilder.ToTable("BookGenres");
				genreBuilder.WithOwner().HasForeignKey("BookId");
				genreBuilder.Property<Guid>("Id");
				genreBuilder.HasKey("Id");
				
			});

			builder.Metadata.FindNavigation(nameof(Book.GenreIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
