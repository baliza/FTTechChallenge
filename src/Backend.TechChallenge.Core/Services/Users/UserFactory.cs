using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Tools;
using System;
using System.Collections.Generic;

namespace Backend.TechChallenge.Core.Services
{
	public class UserFactory : IUserFactory
	{
		private readonly IEmailFixer _eamilFixer;
		private readonly IDictionary<UserType, IBonusCalculator> _bonusCalcultors;

		public UserFactory(IEmailFixer eamilFixer, IDictionary<UserType, IBonusCalculator> bonusCalcultors)
		{
			_eamilFixer = eamilFixer;
			_bonusCalcultors = bonusCalcultors;
		}

		public User CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			var moneyCasted = decimal.Parse(money);
			var typeOfUser = userType.ToUserType();
			var gif = _bonusCalcultors[typeOfUser].Calculate(moneyCasted);

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
	}
}