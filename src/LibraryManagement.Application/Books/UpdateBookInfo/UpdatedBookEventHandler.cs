using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.AuthorAggregate.ValueObjects;
using LibraryManagement.Domain.BookAggregate.Events;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using LibraryManagement.Domain.LocationAggregate;
using LibraryManagement.Domain.LocationAggregate.ValueObjects;
using LibraryManagement.Domain.YearPublicationAggregate;
using LibraryManagement.Domain.YearPublicationAggregate.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Books.UpdateBookInfo;

public class UpdatedBookEventHandler : INotificationHandler<UpdatedBook>
{
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IBaseRepository<PublicationYear> _publicationYearRepository;
    private readonly IBaseRepository<Location> _locationRepository;
    private readonly IBaseRepository<Genre> _genreRepository;

    public UpdatedBookEventHandler(IBaseRepository<Location> locationRepository, IBaseRepository<Author> authorRepository, IBaseRepository<PublicationYear> publicationYearRepository, IBaseRepository<Genre> genreRepository)
    {
        _locationRepository = locationRepository;
        _authorRepository = authorRepository;
        _publicationYearRepository = publicationYearRepository;
        _genreRepository = genreRepository;
    }

    public async Task Handle(UpdatedBook notification, CancellationToken cancellationToken)
    {
        if (notification.LocationId != 0)
        {
            var locationId = await _locationRepository.FindAsync(notification.LocationId);

            if (locationId != null)
            {
                locationId.AddLocationBookId(new LocationBookId(notification.BookId));
                _locationRepository.Update(locationId);
            }
        }

        if (notification.PublicationYearId != 0)
        {
            var publicationYearId = await _publicationYearRepository.FindAsync(notification.BookId)!;
            if (publicationYearId != null)
            {
                publicationYearId.AddBookId(PublicationYearBookId.Create(notification.BookId));
                _publicationYearRepository.Update(publicationYearId);
            }
        }

        if (notification.RemoveAuthorIds != null)
        {
            foreach (var removeAuthorId in notification.RemoveAuthorIds)
            {
                var author = await _authorRepository.FindAsync(removeAuthorId)!;
                if (author != null)
                {
                    author.RemoveBook(AuthorBookId.Create(notification.BookId));
                    _authorRepository.Update(author);
                }
            }
        }

        if (notification.AddAuthorIds != null)
        {
            foreach (var addAuthorId in notification.AddAuthorIds)
            {
                var author = await _authorRepository.FindAsync(addAuthorId)!;
                if (author != null)
                {
                    author.AddBook(AuthorBookId.Create(notification.BookId));
                    _authorRepository.Update(author);
                }
            }
        }

        if (notification.AddGenreIds != null)
        {
            foreach (var addGenreId in notification.AddGenreIds)
            {
                var genre =await _genreRepository.FindAsync(addGenreId)!;
                genre.AddBookId(GenreBookId.Create(notification.BookId));
            }
        }

        if (notification.RemovdeGenreIds != null)
        {
            foreach (var removdeGenreId in notification.RemovdeGenreIds)
            {
                var genre = await _genreRepository.FindAsync(removdeGenreId)!;
                genre.RemoveBookId(GenreBookId.Create(notification.BookId));
            }
        }
        
        await _authorRepository.SaveChangeAsync();
    }
}