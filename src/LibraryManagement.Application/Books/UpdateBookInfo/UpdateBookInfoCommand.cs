using MediatR;
using ErrorOr;

namespace LibraryManagement.Application.Books.UpdateBookInfo;

public record UpdateBookInfoDto(int Id);

public record UpdateBookInfoCommand(
    int BookId,
    string Title,
    string PublisherName,
    string ImageUrl,
    string Description,
    int PublicationYearId,
    int PageCount,
    int LocationId,
    List<int> AuthorIds,
    List<int> GenreIds) : IRequest<ErrorOr<UpdateBookInfoDto>>;