using ErrorOr;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.GenreAggregate;
using MapsterMapper;
using MediatR;
using MediatR.Pipeline;

namespace LibraryManagement.Application.Genres.Get;

public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, ErrorOr<GetGenreDto>>
{
    private readonly IBaseRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;

    public GetGenreQueryHandler(IBaseRepository<Genre> genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<GetGenreDto>> Handle(GetGenreQuery request, CancellationToken cancellationToken)
    {
        var result = await _genreRepository.FindAsync(request.Id);

        if (result == null)
            return Error.NotFound();

        return _mapper.Map<GetGenreDto>(result);
    }
}