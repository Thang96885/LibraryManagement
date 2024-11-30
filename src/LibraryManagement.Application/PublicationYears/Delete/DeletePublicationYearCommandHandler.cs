using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.YearPublicationAggregate;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.Delete;

public class DeletePublicationYearCommandHandler : IRequestHandler<DeletePublicationYearCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

    public DeletePublicationYearCommandHandler(IBaseRepository<PublicationYear> publicationYearRepository)
    {
        _publicationYearRepository = publicationYearRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeletePublicationYearCommand request, CancellationToken cancellationToken)
    {
        var publicatioYear = await _publicationYearRepository.FindAsync(request.Id);
        
        if(publicatioYear == null)
            return Error.NotFound("Publication year not found");

        if (publicatioYear.BookIds.Count > 0)
            return Error.Validation("Publication year still has books");
        
        _publicationYearRepository.Delete(publicatioYear);

         await _publicationYearRepository.SaveChangeAsync();

         return true;
    }
}