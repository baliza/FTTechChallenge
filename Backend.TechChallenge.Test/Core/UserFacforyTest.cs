using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Backend.TechChallenge.Core.Tools;
using System.Collections.Generic;
using Xunit;

namespace Backend.TechChallenge.Test.Core
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class UserFacforyTest
	{
		[Theory]
		[InlineData(UserType.Normal, "124", 138.88)]
		[InlineData(UserType.Normal, "100", 100)]
		[InlineData(UserType.Normal, "85", 85)]
		[InlineData(UserType.Normal, "10", 10)]
		[InlineData(UserType.Normal, "2", 2)]
		[InlineData(UserType.SuperUser, "124", 148.8)]
		[InlineData(UserType.SuperUser, "100", 100)]
		[InlineData(UserType.SuperUser, "85", 85)]
		[InlineData(UserType.Premium, "124", 372)]
		[InlineData(UserType.Premium, "100", 100)]
		[InlineData(UserType.Premium, "85", 85)]
		public void Add_User_As_expected(UserType userType, string money, decimal expectedMoney)
		{
			var sut = new UserFactory(new EmailFixer(), BuildBonusCalculator());
			var result = sut.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", userType.ToString(), money);

			Assert.Equal("Mike", result.Name);
			Assert.Equal("mike@gmail.com", result.Email);
			Assert.Equal("Av. Juan G", result.Address);
			Assert.Equal("+349 1122354215", result.Phone);
			Assert.Equal(userType, result.UserType);
			Assert.Equal(expectedMoney, result.Money);
		}
		private static IDictionary<UserType, IBonusCalculator> BuildBonusCalculator()
		{
			return new Dictionary<UserType, IBonusCalculator> {
				{ UserType.Normal,new BonusCalculatorNormal() },
				{ UserType.Premium,new BonusCalculatorPremiun() },
				{ UserType.SuperUser,new BonusCalculatorSuperUser() }
				};
		}
	}
}