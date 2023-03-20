using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Xunit;

namespace Backend.TechChallenge.Test.Core
{
	public class UserCompareTest
	{
		[Theory]
		[InlineData("Edu", "edu@gmail.com", "Av. edu", "+349 11223555", UserType.Normal, 10, true)]
		[InlineData("Edward", "edu@gmail.com", "Av. edu", "", UserType.Normal, 10, true)]
		[InlineData("Edward", "", "Av. edu", "+349 11223555", UserType.Normal, 10, true)]
		[InlineData("Edu", "", "Av. edu", "", UserType.Normal, 10, true)]
		[InlineData("Edu", "", "Av. edu 2", "", UserType.Normal, 10, false)]
		[InlineData("mike", "mike@gmail.com", "Av Mike", "+349 112", UserType.Normal, 10, false)]
		public void When_UserCompare_Matches_OK(string name, string email, string address, string phone, UserType userType, decimal money, bool expected)
		{
			var newUser = new User
			{
				Name = "Edu",
				Email = "edu@gmail.com",
				Address = "Av. edu",
				Phone = "+349 11223555",
				UserType = UserType.Normal,
				Money = 10m
			};
			var otherUser = new User
			{
				Name = name,
				Email = email,
				Address = address,
				Phone = phone,
				UserType = userType,
				Money = money
			};
			var sut = new UserComparer();
			Assert.Equal(expected, sut.Compare(newUser, otherUser));
		}
	}
}