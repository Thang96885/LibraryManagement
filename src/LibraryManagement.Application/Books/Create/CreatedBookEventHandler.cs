using LibraryManagement.Domain.AuthorAggregate;
using LibraryManagement.Domain.AuthorAggregate.ValueObjects;
using LibraryManagement.Domain.BookAggregate.Events;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.LocationAggregate;
using LibraryManagement.Domain.LocationAggregate.ValueObjects;
using LibraryManagement.Domain.YearPublicationAggregate;
using LibraryManagement.Domain.YearPublicationAggregate.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Books.Create;

public class CreatedBookEventHandler : INotificationHandler<CreatedBook>
{
    private readonly IBaseRepository<Location> _locationRepository;
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

    public CreatedBookEventHandler(IBaseRepository<PublicationYear> publicationYearRepository, IBaseRepository<Author> authorRepository, IBaseRepository<Location> locationRepository)
    {
        _publicationYearRepository = publicationYearRepository;
        _authorRepository = authorRepository;
        _locationRepository = locationRepository;
    }

    public async Task Handle(CreatedBook notification, CancellationToken cancellationToken)
    {
        var publicationYear =  await _publicationYearRepository.FindAsync(notification.Book.PublicationYearId.Value);

        foreach (var authorId in notification.Book.AuthorIds)
        {
            var author = await _authorRepository.FindAsync(authorId.Value);
            author.AddBook(AuthorBookId.Create(notification.Book.Id));
            _authorRepository.Update(author);
        }
        
        var location = await _locationRepository.FindAsync(notification.Book.LocationId.Value);
        
        publicationYear.AddBookId(PublicationYearBookId.Create(notification.Book.Id));
        
        location.AddLocationBookId(LocationBookId.Create(notification.Book.Id));
        
        _locationRepository.Update(location);

        _publicationYearRepository.Update(publicationYear);

        await _locationRepository.SaveChangeAsync();
    }
}