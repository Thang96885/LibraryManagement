using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.Books.UpdateBookInfo;

public record UpdateBookInfoDto(int Id);

public record UpdateBookInfoCommand(
    int BookId, 
    string Title,
    string AuthorName,
    string PublisherName,
    int PublicationYear,
    int PageCount) : IRequest<ErrorOr<UpdateBookInfoDto>>;