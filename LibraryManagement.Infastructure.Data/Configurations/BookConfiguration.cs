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

			builder.Property(b => b.AuthorName)
				.IsRequired(false);

			builder.Property(b => b.PublisherName)
				.HasMaxLength(200)
				.IsRequired();

			// Cấu hình cho các collection
			ConfigGenre(builder);
			ConfigReservation(builder);
			ConfigBorrowRecord(builder);
			ConfigReturnRecord(builder);

			builder.OwnsMany(b => b.BookCopies, bookCopyBuilder =>
			{
				bookCopyBuilder.ToTable("BookCopy");
				bookCopyBuilder.WithOwner().HasForeignKey("BookId");
				bookCopyBuilder.HasKey(b => b.Id);

				bookCopyBuilder.Property(b => b.Id)
				.HasMaxLength(13);

				bookCopyBuilder.Property(b => b.PhysicalCondition)
				.HasConversion<string>();

				bookCopyBuilder.Property(b => b.Status)
				.HasConversion<string>();
			});
		}

		private static void ConfigBorrowRecord(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.BorrowRecordIds, borrowRecordBuilder =>
			{
				borrowRecordBuilder.WithOwner().HasForeignKey("BookId");
				borrowRecordBuilder.Property(x => x.Value).HasColumnName("BorrowRecordId");
				borrowRecordBuilder.HasKey("Value", "BookId");
				borrowRecordBuilder.ToTable("BookBorrowRecords");
			});

			builder.Metadata.FindNavigation(nameof(Book.BorrowRecordIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private static void ConfigReservation(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.BookReservationId, bookReservationBuilder =>
			{
				bookReservationBuilder.WithOwner().HasForeignKey("BookId");
				bookReservationBuilder.Property(x => x.Value).HasColumnName("BookReservationId");
				bookReservationBuilder.HasKey("Value", "BookId");
				bookReservationBuilder.ToTable("BookReservations");
			});

			builder.Metadata.FindNavigation(nameof(Book.BookReservationId)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private static void ConfigReturnRecord(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.ReturnRecordIds, returnRecordBuilder =>
			{
				returnRecordBuilder.WithOwner().HasForeignKey("BookId");
				returnRecordBuilder.Property(x => x.Value).HasColumnName("BookReturnRecordId");
				returnRecordBuilder.HasKey("Value", "BookId");
				returnRecordBuilder.ToTable("BookReturnRecords");
			});

			builder.Metadata.FindNavigation(nameof(Book.ReturnRecordIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private static void ConfigGenre(EntityTypeBuilder<Book> builder)
		{
			builder.OwnsMany(b => b.GenreIds, genreBuilder =>
			{
				genreBuilder.ToTable("BookGenres");
				genreBuilder.WithOwner().HasForeignKey("BookId");
				genreBuilder.Property(x => x.Value).HasColumnName("BookGenreId");
				genreBuilder.HasKey("Value", "BookId");
				
			});

			builder.Metadata.FindNavigation(nameof(Book.GenreIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
