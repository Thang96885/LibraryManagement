using MediatR;

namespace LibraryManagement.Application.PatronTypes.Delete;

public record DeletePatronTypeCommand(int Id) : IRequest<ErrorOr.ErrorOr<bool>>;