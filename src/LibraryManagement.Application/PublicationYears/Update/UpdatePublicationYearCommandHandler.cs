using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.YearPublicationAggregate;
using MediatR;

namespace LibraryManagement.Application.PublicationYears.Update;

public class UpdatePublicationYearCommandHandler : IRequestHandler<UpdatePublicationYearCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

    public UpdatePublicationYearCommandHandler(IBaseRepository<PublicationYear> publicationYearRepository)
    {
        _publicationYearRepository = publicationYearRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdatePublicationYearCommand request, CancellationToken cancellationToken)
    {
        var publicationYear = await _publicationYearRepository.FindAsync(request.PublicationYearId);
        
        if(publicationYear == null)
            return Error.NotFound("Publication year not found");
        
        publicationYear.Update(request.Year);

        await _publicationYearRepository.SaveChangeAsync();

        return true;
    }
}