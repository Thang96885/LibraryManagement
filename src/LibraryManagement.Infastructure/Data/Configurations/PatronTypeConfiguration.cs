using LibraryManagement.Domain.PatronTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infastructure.Data.Configurations;

public class PatronTypeConfiguration : IEntityTypeConfiguration<PatronType>
{
    public void Configure(EntityTypeBuilder<PatronType> builder)
    {
        builder.ToTable("PatronType");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasMaxLength(30);

        builder.OwnsMany(p => p.PatronIds, builder =>
        {
            builder.ToTable("PatronTypePatronId");

            builder.Property(x => x.Value)
                .HasColumnName("PatronId");

            builder.WithOwner().HasForeignKey("PatronTypeId");

            builder.HasKey("PatronTypeId", "Value");
        });
    }
}