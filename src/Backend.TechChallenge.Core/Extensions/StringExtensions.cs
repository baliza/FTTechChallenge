
using Backend.TechChallenge.Core.Models;

namespace System
{
	public static class StringExtensions
	{
		public static UserType ToUserType(this string self)
		{
			Enum.TryParse(self, out UserType typeOfUser);
			return typeOfUser;
		}
	}
}