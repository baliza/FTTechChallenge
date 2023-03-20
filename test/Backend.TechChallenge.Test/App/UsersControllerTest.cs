using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Backend.TechChallenge.Test
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class UsersControllerTest
	{
		private Mock<IRequestValidator> _moqValidator;
		private Mock<IUserService> _moqUserService;

		private Mock<ILogger<UsersController>> _mockUsersControllerLogger;

		public UsersControllerTest()
		{
			_moqValidator = new Mock<IRequestValidator>();
			_moqUserService = new Mock<IUserService>();
			_mockUsersControllerLogger = new Mock<ILogger<UsersController>>();
			SetValidator();
		}

		[Fact]
		public void When_Everything_OK_It_Add_User()
		{
			SetServiceOk();
			var userController = CreateSut();
			var result = userController.CreateUser("Edu", "edu@gmail.com", "Av. edu", "+349 11223555", "Normal", "10").Result;

			Assert.True(result.IsSuccess);
			Assert.Equal("User Created", result.Message);
		}

		[Fact]
		public void When_Not_OK_It_Does_Not_Add_User()
		{
			SetServiceFailed(" The user is duplicated");
			var userController = CreateSut();
			var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.False(result.IsSuccess);
			Assert.Equal(" The user is duplicated", result.Message);
		}

		private UsersController CreateSut()
		{
			return new UsersController(_moqValidator.Object, _moqUserService.Object, _mockUsersControllerLogger.Object);
		}

		private void SetValidator()
		{
			_moqValidator.Setup(s => s.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new OperationResult());
		}

		private void SetServiceOk()
		{
			_moqUserService.Setup(s => s.AddUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(
					Task.FromResult(new OperationResult()));
		}

		private void SetServiceFailed(string text)
		{
			_moqUserService.Setup(s => s.AddUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(
					Task.FromResult(new OperationResult(text)));
		}
	}
}