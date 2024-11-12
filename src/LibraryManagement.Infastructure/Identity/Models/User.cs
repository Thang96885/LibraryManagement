using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Data.Identity.Models
{
	public class User : IdentityUser
	{
		[AllowNull]
		public int? PatronId { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }
	}
}
