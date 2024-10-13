using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Infastructure.Data.Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infastructure.Identity.Services
{
	public class TokenGennerator : ITokenGennerator
	{
		private readonly IConfiguration _config;

		public TokenGennerator(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		public string GenerateJwt(UserInfo userInfo)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Key").Value);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, userInfo.UserName),
				}),
				Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config.GetSection("Jwt:Expires").Value)),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			foreach (var role in userInfo.Roles)
			{
				tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
			}

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
