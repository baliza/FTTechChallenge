using Backend.TechChallenge.Core.Models;
using System;

namespace Backend.TechChallenge.Core.Services
{
	public class UserFactory : IUserFactory
	{
		private readonly IEmailFixer _eamilFixer;

		public UserFactory(IEmailFixer eamilFixer)
		{
			_eamilFixer = eamilFixer;
		}

		public User CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			var moneyCasted = decimal.Parse(money);
			var gif = 0m;
			var typeOfUser = userType.ToUserType();
			if (typeOfUser == UserType.Normal)
			{
				gif = CalculateNormalUserGif(moneyCasted);
			}
			if (typeOfUser == UserType.SuperUser)
			{
				gif = CalculateSuperUserGif(moneyCasted);
			}
			if (typeOfUser == UserType.Premium)
			{
				gif = CalculatePremiunGif(moneyCasted);
			}
			var newUser = new User
			{
				Name = name,
				Email = _eamilFixer.Amend(email),
				Address = address,
				Phone = phone,
				UserType = typeOfUser,
				Money = moneyCasted + gif
			};

			return newUser;
		}

		private static decimal CalculatePremiunGif(decimal money)
		{
			if (money > 100)
			{
				return money * 2;
			}
			return 0;
		}

		private static decimal CalculateSuperUserGif(decimal money)
		{
			if (money > 100)
			{
				var percentage = Convert.ToDecimal(0.20);
				return money * percentage;
			}

			return 0;
		}

		private static decimal CalculateNormalUserGif(decimal money)
		{
			if (money > 100)
			{
				if (money > 100)
				{
					var percentage = Convert.ToDecimal(0.12);
					return money * percentage;
				}
				if (money < 100)
				{
					if (money > 10)
					{
						var percentage = Convert.ToDecimal(0.8);
						return money * percentage;
					}
				}
			}
			return 0;
		}
	}
}