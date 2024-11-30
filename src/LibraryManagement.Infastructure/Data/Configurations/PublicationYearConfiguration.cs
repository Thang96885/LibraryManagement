using LibraryManagement.Domain.YearPublicationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infastructure.Data.Configurations;

public class PublicationYearConfiguration : IEntityTypeConfiguration<PublicationYear>
{
    public void Configure(EntityTypeBuilder<PublicationYear> builder)
    {
        builder.ToTable("PublicationYears");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Year);

        builder.OwnsMany(p => p.BookIds, builder =>
        {
            builder.ToTable("PublicationYearBookIds");

            builder.WithOwner().HasForeignKey("PublicationYearId");

            builder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("BookId");

            builder.HasKey("PublicationYearId", "Value");
        });
    }
}