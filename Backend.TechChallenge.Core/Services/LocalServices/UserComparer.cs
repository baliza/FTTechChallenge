using Backend.TechChallenge.Core.Models;

namespace Backend.TechChallenge.Core.Services
{
	public class UserComparer
	{
		public bool Compare(User other, User another)
		{
			if (other.Email == another.Email || other.Phone == another.Phone)
			{
				return true;
			}
			return other.Name == another.Name
				&& other.Address == another.Address;
		}
	}
}