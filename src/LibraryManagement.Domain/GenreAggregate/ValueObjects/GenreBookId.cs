using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.GenreAggregate.ValueObjects
{
	public class GenreBookId : ValueObject
	{
		public int Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private GenreBookId(int value)
		{
			Value = value;
		}

		public static GenreBookId Create(int value) 
		{ 
			return new(value); 
		}
	}
}
