﻿using LibraryManagement.Domain.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects
{
	public class BorrowRecordPatronId : ValueObject
	{
		public Guid Value { get; private set; }
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		private BorrowRecordPatronId(Guid value)
		{
			Value = value;
		}

		public static BorrowRecordPatronId Create(Guid value)
		{
			return new(value);
		}
	}
}
