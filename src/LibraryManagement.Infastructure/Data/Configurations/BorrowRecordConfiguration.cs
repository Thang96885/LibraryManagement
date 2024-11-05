using System.Text.Json;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.OwnsMany(x => x.BookIds, borrowRecordBookBuilder =>
            {
                borrowRecordBookBuilder.ToTable("BorrowRecordBookId");
                borrowRecordBookBuilder.WithOwner().HasForeignKey("BorrowRecordId");
                borrowRecordBookBuilder.Property(x => x.BookId)
                    .HasColumnName("BookId");

                borrowRecordBookBuilder.Property(x => x.BookCopyIds)
                    .HasColumnName("BookCopyIds")
                    .HasConversion((bookcopyids) => JsonSerializer.Serialize(bookcopyids, new JsonSerializerOptions()),
                        value => JsonSerializer.Deserialize<List<string>>(value, new JsonSerializerOptions()))
                    .HasMaxLength(500);
                
                borrowRecordBookBuilder.HasKey("BookId", "BorrowRecordId");

                
            });
        }
    }
}
