using Backend.TechChallenge.Api.Controllers;

using Xunit;

namespace Backend.TechChallenge.Test
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class UsersControllerTest
	{
		[Fact]
		public void When_Everything_OK_It_Creates_User()
		{
			var userController = new UsersController();

			var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.True(result.IsSuccess);
			Assert.Equal("User Created", result.Message);
		}

		[Fact]
		public void When_Not_OK_It_Does_Not_Creates_User()
		{
			var userController = new UsersController();

			var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

			Assert.False(result.IsSuccess);
			Assert.Equal(" The user is duplicated", result.Message);
		}
	}
}