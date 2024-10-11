using Mapster;
using LibraryManagement.Application.BookServices.Common;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BookAggregate.ValueObjects;

namespace LibraryManagement.Application.Common.Mappings
{
    public class BookMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Book, BookDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.AuthorName, src => string.Empty) // Cần xử lý riêng
                .Map(dest => dest.PublisherName, src => src.PublisherName)
                .Map(dest => dest.PublicationYear, src => src.PublicationYear)
                .Map(dest => dest.PageCount, src => src.PageCount)
                .Map(dest => dest.NumberOfCopy, src => src.NumberOfCopy)
                .Map(dest => dest.NumberAvailable, src => src.NumberAvailable)
                .Map(dest => dest.Genres, src => new List<GenreDto>()); // Cần xử lý riêng

            config.NewConfig<BookGenreId, GenreDto>()
                .Map(dest => dest.Id, src => src.Value)
                .Map(dest => dest.Name, src => string.Empty); // Cần xử lý riêng
        }
    }
}