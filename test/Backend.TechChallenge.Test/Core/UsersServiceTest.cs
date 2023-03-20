using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.TechChallenge.Test.Core
{
	public class UsersServiceTest
	{
		private Mock<IUserFactory> _moqUserFactory;
		private Mock<IUsersRepository> _moqUsersRepository;
		private Mock<ILogger<UserService>> _mockUsersControllerLogger;

		public UsersServiceTest()
		{
			_moqUserFactory = new Mock<IUserFactory>();
			_moqUsersRepository = new Mock<IUsersRepository>();
			_mockUsersControllerLogger = new Mock<ILogger<UserService>>();
		}

		[Fact]
		public void When_Everything_OK_It_Add_User()
		{
			SetFactory("Edu", "edu@gmail.com", "Av. edu", "+349 11223555", UserType.Normal, 10m);
			SetRepoOk();
			var userController = CreateSut();
			var result = userController.AddUser("Edu", "edu@gmail.com", "Av. edu", "+349 11223555", "Normal", "10").Result;

			Assert.True(result.IsSuccess);
			Assert.Null(result.Message);
		}

		[Fact]
		public void When_Not_OK_It_Does_Not_Add_User()
		{
			SetFactory("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", UserType.Normal, 124m);
			SetRepoOk();

			var userController = CreateSut();
			var result = userController.AddUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.False(result.IsSuccess);
			Assert.Equal(" The user is duplicated", result.Message);
		}

		[Fact]
		public void When_GetALL_Returns_Users()
		{
			SetRepoOk();
			var userController = CreateSut();
			var result = userController.GetAllUsers().Result;

			Assert.True(result.IsSuccess);
			Assert.Equal(string.Empty, result.Message);
			Assert.Equal(2, result.Data.Count);
		}

		private IUserService CreateSut()
		{
			return new UserService(_moqUsersRepository.Object, _moqUserFactory.Object, _mockUsersControllerLogger.Object);
		}

		private void SetFactory(string name, string email, string address, string phone, UserType userType, decimal money)
		{
			_moqUserFactory.Setup(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(
					new User { Name = name, Email = email, Address = address, Phone = phone, UserType = userType, Money = money });
		}

		private void SetRepoOk()
		{
			_moqUsersRepository.Setup(s => s.Save(It.IsAny<User>()));
			_moqUsersRepository.Setup(s => s.GetAll()).Returns(Task.FromResult(new List<User>{
				new User { Name = "Mike", Email = "Mike@gmail.com", Address = "Av. Mike", Phone = "+349 25635", UserType = UserType.Normal, Money = 10m },
				new User { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = UserType.Normal, Money = 124m }
			}));
		}
	}
}