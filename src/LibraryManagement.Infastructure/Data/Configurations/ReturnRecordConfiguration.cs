using LibraryManagement.Domain.ReturnRecordAggregate;
using LibraryManagement.Domain.ReturnRecordAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Data.Configurations
{
    public class ReturnRecordConfiguration : IEntityTypeConfiguration<ReturnRecord>
    {
        public void Configure(EntityTypeBuilder<ReturnRecord> builder)
        {
            builder.ToTable("ReturnRecords");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.BorrowRecordId)
                .HasConversion(borrowRecordId => borrowRecordId.Value,
                value => ReturnRecordBorrowRecordId.Create(value));

            builder.Property(x => x.PatronId)
                .HasConversion(patronId => patronId.Value,
                value => ReturnRecordPatronId.Create(value));

            builder.Property(x => x.LateFee)
                .IsRequired(false);

            BookReturnStatusConfig(builder);
        }

        private static void BookReturnStatusConfig(EntityTypeBuilder<ReturnRecord> builder)
        {
            builder.OwnsMany(x => x.BookReturnStatus, returnStatusBuilder =>
            {
                returnStatusBuilder.ToTable("BookReturnStatus");

                returnStatusBuilder.WithOwner().HasForeignKey("ReturnRecordId");

                returnStatusBuilder.Property(x => x.BookId)
                .HasConversion(bookId => bookId.Value, value => ReturnRecordBookCopyId.Create(value))
                    .HasMaxLength(13);

                returnStatusBuilder.Property(x => x.Condition)
                    .HasConversion<string>();

                returnStatusBuilder.HasKey("BookId", "ReturnRecordId");
            });

            builder.Metadata.FindNavigation(nameof(ReturnRecord.BookReturnStatus))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
