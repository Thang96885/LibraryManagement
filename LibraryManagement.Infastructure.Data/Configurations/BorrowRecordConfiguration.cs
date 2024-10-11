using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Configurations
{
	public class BorrowRecordConfiguration : IEntityTypeConfiguration<BorrowRecord>
	{
		public void Configure(EntityTypeBuilder<BorrowRecord> builder)
		{
			builder.ToTable("BorrowRecords");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever();

			builder.Property(x => x.PatronId)
				.HasConversion(id => id.Value, value => BorrowRecordPatronId.Create(value));

			builder.Property(x => x.ReturnRecordId)
				.ValueGeneratedNever()
				.IsRequired(false)
				.HasConversion(
				id => id.Value,
				value => BorrowRecordReturnRecordId.Create(value));

			builder.OwnsMany(x => x.BookIds, BorrowRecordBookBuilder =>
			{
				BorrowRecordBookBuilder.ToTable("BorrowRecordBookId");
				BorrowRecordBookBuilder.WithOwner().HasForeignKey("BorrowRecordId");
				BorrowRecordBookBuilder.Property(x => x.Value)
				.HasColumnName("BookId");
				BorrowRecordBookBuilder.HasKey("Value", "BorrowRecordId");
			});
		}
	}
}
