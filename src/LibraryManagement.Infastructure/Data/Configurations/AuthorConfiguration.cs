using LibraryManagement.Domain.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infastructure.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.OwnsMany(x => x.BookIds, builder =>
        {
            builder.ToTable("AuthorBookIds");

            builder.WithOwner().HasForeignKey("AuthorId");
            builder.Property(x => x.Value)
                .HasColumnName("BookId");

            builder.HasKey("AuthorId", "Value");
        });
    }
}