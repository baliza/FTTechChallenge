using System;

namespace Backend.TechChallenge.Api.Controllers
{
	public static class UserFactory
	{
		public static User CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			var newUser = new User
			{
				Name = name,
				Email = email,
				Address = address,
				Phone = phone,
				UserType = userType,
				Money = decimal.Parse(money)
			};

			if (newUser.UserType == "Normal")
			{
				if (decimal.Parse(money) > 100)
				{
					var percentage = Convert.ToDecimal(0.12);
					//If new user is normal and has more than USD100
					var gif = decimal.Parse(money) * percentage;
					newUser.Money = newUser.Money + gif;
				}
				if (decimal.Parse(money) < 100)
				{
					if (decimal.Parse(money) > 10)
					{
						var percentage = Convert.ToDecimal(0.8);
						var gif = decimal.Parse(money) * percentage;
						newUser.Money = newUser.Money + gif;
					}
				}
			}
			if (newUser.UserType == "SuperUser")
			{
				if (decimal.Parse(money) > 100)
				{
					var percentage = Convert.ToDecimal(0.20);
					var gif = decimal.Parse(money) * percentage;
					newUser.Money = newUser.Money + gif;
				}
			}
			if (newUser.UserType == "Premium")
			{
				if (decimal.Parse(money) > 100)
				{
					var gif = decimal.Parse(money) * 2;
					newUser.Money = newUser.Money + gif;
				}
			}
			var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

			var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

			aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

			newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });
			return newUser;
		}
	}

	//Validate errors
}