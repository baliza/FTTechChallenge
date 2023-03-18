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
		private Mock<IUserFactory> moqUserFactory;

		public UsersControllerTest()
		{
			moqValidator = new Mock<IRequestValidator>();
			moqUserFactory = new Mock<IUserFactory>();
			moqValidator.Setup(s => s.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new OperationResult());
			moqUserFactory.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
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
			moqUserFactory.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(
					new User { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = UserType.Normal, Money = 124m }); 
			return new UsersController(moqValidator.Object, moqUserFactory.Object);

		}
	}
}