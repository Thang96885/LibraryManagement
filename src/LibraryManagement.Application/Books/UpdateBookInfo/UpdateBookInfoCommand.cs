using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.Books.UpdateBookInfo;

public record UpdateBookInfoDto(int Id);

public record UpdateBookInfoCommand(
    int BookId, 
    string Title,
    int AuthorId,
    string PublisherName,
    int PublicationYearId,
    int PageCount,
    int LocationId) : IRequest<ErrorOr<UpdateBookInfoDto>>;