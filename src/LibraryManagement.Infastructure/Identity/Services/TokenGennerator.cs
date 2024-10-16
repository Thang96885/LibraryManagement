using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Infastructure.Data.Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
				Audience = _config.GetSection("Jwt:Audience").Value,
				Issuer = _config.GetSection("Jwt:Issuer").Value,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			foreach (var role in userInfo.Roles)
			{
				tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
			}

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public bool ValidateJwt(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Key").Value);

			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = _config.GetSection("Jwt:Issuer").Value,
					ValidAudience = _config.GetSection("Jwt:Audience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				return true; // Token hợp lệ	
			}
			catch
			{
				return false; // Token không hợp lệ hoặc có lỗi khi xử lý
			}
		}

		public UserInfo? DecodeJwt(string token)
		{
			if (ValidateJwt(token) == false)
				return null;
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Key").Value);

			try
			{
				var jwtToken = tokenHandler.ReadJwtToken(token);
				
				// Tìm claim với type là "nameid" thay vì ClaimTypes.NameIdentifier
				var userName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
				var roles = jwtToken.Claims.Where(claim => claim.Type == "role").Select(claim => claim.Value).ToList();

				if (string.IsNullOrEmpty(userName))
					return null;

				return new UserInfo(userName, roles);
			}
			catch
			{
				return null; // Token không hợp lệ hoặc có lỗi khi xử lý
			}
		}
	}
}
