﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Identity.Models
{
	public class User : IdentityUser<Guid>
	{
		public string PatronId { get; set; }
	}
}
