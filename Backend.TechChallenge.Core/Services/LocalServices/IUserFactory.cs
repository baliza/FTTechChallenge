using Backend.TechChallenge.Core.Models;

namespace Backend.TechChallenge.Core.Services
{
	public interface IUserFactory
	{
		User CreateUser(string name, string email, string address, string phone, string userType, string money);
	}
}