using MediatR;
using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.YearPublicationAggregate;

namespace LibraryManagement.Application.PublicationYears.Create;

public class CreatePublicationYearCommandHandler : IRequestHandler<CreatePublicationYearCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<PublicationYear> _publicationYearRepository;

    public CreatePublicationYearCommandHandler(IBaseRepository<PublicationYear> publicationYearRepository)
    {
        _publicationYearRepository = publicationYearRepository;
    }

    public async Task<ErrorOr<bool>> Handle(CreatePublicationYearCommand request, CancellationToken cancellationToken)
    {
        var publicationYearQuery = _publicationYearRepository.GetQueryable();
        publicationYearQuery = publicationYearQuery.Where(p => p.Year == request.year);

        if (publicationYearQuery.Any())
        {
            return Error.Validation("The year is already exist");
        }
        
        var publicationYear = new PublicationYear(request.year);
        
        _publicationYearRepository.Add(publicationYear);

        await _publicationYearRepository.SaveChangeAsync();

        return true;
    }
}