namespace LibraryManagement.Domain.Common.Interface.DomainServices;

public interface IGenreService
{
    bool IsGenreNameUnique(string genreName);
}