using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.Books.UpdateBookInfo;

public record UpdateBookInfoDto(int Id);

public record UpdateBookInfoCommand(
    int BookId, 
    string Title,
    string PublisherName,
    int PublicationYearId,
    int PageCount,
    int LocationId,
    List<int> RemoveAuthorIds,
    List<int> AddAuthorIds) : IRequest<ErrorOr<UpdateBookInfoDto>>;