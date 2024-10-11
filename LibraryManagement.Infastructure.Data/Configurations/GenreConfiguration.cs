using LibraryManagement.Domain.GenreAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Configurations
{
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.ToTable("Genres");

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.Name).IsUnique();
			ConfigBookId(builder);
		}

		private static void ConfigBookId(EntityTypeBuilder<Genre> builder)
		{
			builder.OwnsMany(x => x.BookIds, genreBookBuilder =>
			{
				genreBookBuilder.ToTable("GenreBookIds");
				genreBookBuilder.WithOwner().HasForeignKey("GenreId");
				genreBookBuilder.Property(x => x.Value).HasColumnName("BookId");
				genreBookBuilder.HasKey("Value", "GenreId");
			});
			builder.Metadata.FindNavigation(nameof(Genre.BookIds)).SetPropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}
