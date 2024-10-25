using LibraryManagement.Domain.Common.BaseModels;
using LibraryManagement.Domain.GenreAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.Common.Error;
using LibraryManagement.Domain.Common.Interface.DomainServices;

namespace LibraryManagement.Domain.GenreAggregate
{
	public class Genre : AggregateRoot
	{
		private readonly List<GenreBookId> _bookIds = new();

        public string Name { get; private set; }
        public IReadOnlyList<GenreBookId> BookIds => _bookIds.AsReadOnly();

		private Genre(Guid Id, string Name)
		{
			this.Id = Id;
			this.Name = Name;
		}

		public static Genre Create(string Name, IGenreService genreService)
		{
			var isNameUnique = genreService.IsGenreNameUnique(Name);
			
			if(isNameUnique == false)
				throw new DuplicateNameException("Genre name is already taken");
			
			return new Genre(Guid.NewGuid(), Name);
		}

		private Genre()
		{

		}
	}
}
