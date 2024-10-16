using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Interface
{
	public interface ITokenGennerator
	{
		public string GenerateJwt(UserInfo userInfo);
		public string GenerateRefreshToken();
		public UserInfo? DecodeJwt(string token);
	}

	public record UserInfo(string UserName, List<string> Roles);

}
