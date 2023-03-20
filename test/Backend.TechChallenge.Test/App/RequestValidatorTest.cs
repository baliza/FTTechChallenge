using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Api.Services;
using Xunit;

namespace Backend.TechChallenge.Test
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class RequestValidatorTest
	{
		[Fact]
		public void Validates_OK()
		{
			var sut = GetSut();

			var result = sut.IsValid("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

			Assert.True(result.IsSuccess);
			Assert.Null(result.Message);
		}

		private static RequestValidator GetSut()
		{
			return new RequestValidator();
		}

		[Theory]
		[InlineData(null, "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124", " The name is required")]
		[InlineData("", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124", " The name is required")]
		[InlineData("Mike", null, "Av. Juan G", "+349 1122354215", "Normal", "124", " The email is required")]
		[InlineData("Mike", "", "Av. Juan G", "+349 1122354215", "Normal", "124", " The email is required")]
		[InlineData("Mike", "mike@gmail.com", null, "+349 1122354215", "Normal", "124", " The address is required")]
		[InlineData("Mike", "mike@gmail.com", "", "+349 1122354215", "Normal", "124", " The address is required")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", null, "Normal", "124", " The phone is required")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", "", "Normal", "124", " The phone is required")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", null, "124", " The userType is required")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "", "124", " The userType is required")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", null, " The money has to be a decimal value")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "", " The money has to be a decimal value")]
		[InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "asd", " The money has to be a decimal value")]
		public void When_Empty_Value_Fails(string name, string email, string address, string phone, string userType, string money, string expected)
		{
			var sut = GetSut();

			var result = sut.IsValid(name, email, address, phone, userType, money);

			Assert.False(result.IsSuccess);
			Assert.Equal(expected, result.Message);
		}
	}
}