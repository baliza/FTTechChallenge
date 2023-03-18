using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Core.Services;
using Xunit;

namespace Backend.TechChallenge.Test.Core
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class UserFacforyTest
	{
		[Fact]
		public void Creates_user_ok()
		{
			var sut = new UserFactory(new EmailFixer());
			var result = sut.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

			Assert.Equal("Mike", result.Name);
			Assert.Equal("mike@gmail.com", result.Email);
			Assert.Equal("Av. Juan G", result.Address);
			Assert.Equal("+349 1122354215", result.Phone);
			Assert.Equal(138.88m, result.Money);
			Assert.Equal("Normal", result.UserType.ToString());
		}
	}
}