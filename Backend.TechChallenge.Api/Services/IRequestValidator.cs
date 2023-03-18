using Backend.TechChallenge.Core.Models;

namespace Backend.TechChallenge.Api.Services
{
	public interface IRequestValidator
	{
		OperationResult IsValid(string name, string email, string address, string phone, string userType, string money);
	}
}