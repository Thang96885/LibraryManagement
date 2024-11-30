using ErrorOr;
using MediatR;

namespace LibraryManagement.Application.Authors.List;


public record ListAuthorRecord(int Id,
    string Name, int NumberOfBooks);
public record ListAuthorDto(
    List<ListAuthorRecord> Records,
    int TotalNumberOfAuthors);

public record ListAuthorQuery(int page,
    int pageSize, string searchName = "") : IRequest<ErrorOr<ListAuthorDto>>;