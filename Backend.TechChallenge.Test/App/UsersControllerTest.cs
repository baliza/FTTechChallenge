using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Moq;
using Xunit;

namespace Backend.TechChallenge.Test
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class UsersControllerTest
	{
		private Mock<IRequestValidator> moqValidator;
		private Mock<IUserFactory> moqUserService;

		public UsersControllerTest()
		{
			moqValidator = new Mock<IRequestValidator>();
			moqUserService = new Mock<IUserFactory>();
			moqValidator.Setup(s => s.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new OperationResult());
			moqUserService.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
		}

		[Fact]
		public void When_Everything_OK_It_Creates_User()
		{
			var userController = CreateSut();

			var result = userController.CreateUser("Mike2", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.True(result.IsSuccess);
			Assert.Equal("User Created", result.Message);
		}

		[Fact]
		public void When_Not_OK_It_Does_Not_Creates_User()
		{
			var userController = CreateSut();

			var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.False(result.IsSuccess);
			Assert.Equal(" The user is duplicated", result.Message);
		}

		private UsersController CreateSut()
		{
			moqValidator.Setup(s => s.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new OperationResult());
			moqUserService.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
			return new UsersController(moqValidator.Object, moqUserService.Object);
		}
	}
}