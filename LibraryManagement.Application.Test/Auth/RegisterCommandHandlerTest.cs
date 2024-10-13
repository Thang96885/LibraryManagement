using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Auth.Register;
using LibraryManagement.Application.Common.Interface;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using Moq;
using Xunit;
using ErrorOr;
using LibraryManagement.Domain.PatronAggregate.ValueObjects;

namespace LibraryManagement.Application.Test.Auth
{
	public class RegisterCommandHandlerTest
	{
		private readonly Mock<IIdentityService> _mockIdentityService;
		private readonly Mock<IBaseRepository<Patron>> _mockPatronRepository;
		private readonly Mock<ITokenGennerator> _mockTokenGenerator;
		private readonly RegisterCommandHandler _handler;

		public RegisterCommandHandlerTest()
		{
			_mockIdentityService = new Mock<IIdentityService>();
			_mockPatronRepository = new Mock<IBaseRepository<Patron>>();
			_mockTokenGenerator = new Mock<ITokenGennerator>();
			_handler = new RegisterCommandHandler(_mockIdentityService.Object, _mockPatronRepository.Object, _mockTokenGenerator.Object);
		}

		[Fact]
		public async Task Handle_WhenPatronDoesNotExist_ReturnsFailure()
		{
			// Arrange
			var command = new RegisterCommand(Guid.NewGuid().ToString(), "username", "password");
			_mockPatronRepository.Setup(r => r.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Patron)null);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal("PatronId does not exsist", result.FirstError.Code);
		}

		[Fact]
		public async Task Handle_WhenPatronAlreadyHasAccount_ReturnsFailure()
		{
			// Arrange
			var patronId = Guid.NewGuid().ToString();
			var command = new RegisterCommand(patronId, "username", "password");
			_mockPatronRepository.Setup(r => r.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Patron.Create("name", "email", "phoneNumber", PatronAddress.Create("street", "city", "state", "zipCode")));
			_mockIdentityService.Setup(s => s.FindUserByPatronIdAsync(patronId)).ReturnsAsync(new UserInfo("Name", new List<string>()));

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.True(result.IsError);
			Assert.Equal("Patron alrealdy has account", result.FirstError.Code);
		}

		[Fact]
		public async Task Handle_WhenRegistrationSucceeds_ReturnsToken()
		{
			// Arrange
			var patronId = Guid.NewGuid().ToString();
			var command = new RegisterCommand(patronId, "username", "password");
			var patron = Patron.Create("name", "email", "phoneNumber", PatronAddress.Create("street", "city", "state", "zipCode"));
			_mockPatronRepository.Setup(r => r.FindAsync(It.IsAny<Guid>())).ReturnsAsync(patron);
			_mockIdentityService.Setup(s => s.FindUserByPatronIdAsync(patronId)).ReturnsAsync((UserInfo)null);
			_mockIdentityService.Setup(s => s.SignInAsync(It.IsAny<RegisterInfo>())).ReturnsAsync(new UserInfo("userName", new List<string>()));
			_mockTokenGenerator.Setup(t => t.GenerateJwt(It.IsAny<UserInfo>())).Returns("jwt-token");
			_mockTokenGenerator.Setup(t => t.GenerateRefreshToken()).Returns("refresh-token");

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.False(result.IsError);
			Assert.Equal("jwt-token", result.Value.jwtToken);
			Assert.Equal("refresh-token", result.Value.refreshToken);
		}
	}
}
