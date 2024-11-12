using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infastructure.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);

        builder.OwnsMany(x => x.LocationBookIds, builder =>
        {
            builder.ToTable("LocationBookId");
            builder.Property(x => x.Value)
                .HasColumnName("BookId");
            builder.WithOwner().HasForeignKey("LocationId");
        });
    }
}