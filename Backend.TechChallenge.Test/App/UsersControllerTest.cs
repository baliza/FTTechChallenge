using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.TechChallenge.Test
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class UsersControllerTest
	{
		private Mock<IRequestValidator> moqValidator;
		private Mock<IUserFactory> moqUserFactory;
		private Mock<IUsersRepository> moqUsersRepository;

		private Mock<ILogger<UsersController>> _mockUsersControllerLogger;

		public UsersControllerTest()
		{
			moqValidator = new Mock<IRequestValidator>();
			moqUserFactory = new Mock<IUserFactory>();
			moqUsersRepository = new Mock<IUsersRepository>();
			_mockUsersControllerLogger = new Mock<ILogger<UsersController>>();
			SetRepo();
			SetValidator();
		}

		[Fact]
		public void When_Everything_OK_It_Creates_User()
		{
			SetFactory("Edu", "edu@gmail.com", "Av. edu", "+349 11223555", UserType.Normal, 10m);
			var userController = CreateSut();
			var result = userController.CreateUser("Edu", "edu@gmail.com", "Av. edu", "+349 11223555", "Normal", "10").Result;

			Assert.True(result.IsSuccess);
			Assert.Equal("User Created", result.Message);
		}

		[Fact]
		public void When_Not_OK_It_Does_Not_Creates_User()
		{
			SetFactory("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", UserType.Normal, 124m);

			var userController = CreateSut();
			var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.False(result.IsSuccess);
			Assert.Equal(" The user is duplicated", result.Message);
		}

		private UsersController CreateSut()
		{
			return new UsersController(moqValidator.Object, moqUserFactory.Object, moqUsersRepository.Object, _mockUsersControllerLogger.Object);
		}

		private void SetValidator()
		{
			moqValidator.Setup(s => s.IsValid(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new OperationResult());
		}

		private void SetFactory(string name, string email, string address, string phone, UserType userType, decimal money)
		{
			moqUserFactory.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(
					new User { Name = name, Email = email, Address = address, Phone = phone, UserType = userType, Money = money });
		}

		private void SetRepo()
		{
			moqUsersRepository.Setup(s => s.Save(It.IsAny<User>()));
			moqUsersRepository.Setup(s => s.GetAll()).Returns(Task.FromResult(new List<User>{
				new User { Name = "Mike", Email = "Mike@gmail.com", Address = "Av. Mike", Phone = "+349 25635", UserType = UserType.Normal, Money = 10m },
				new User { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = UserType.Normal, Money = 124m }
			}));
		}
	}
}