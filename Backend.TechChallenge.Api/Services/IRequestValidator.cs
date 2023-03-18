namespace Backend.TechChallenge.Api.Controllers
{
	public interface IRequestValidator
	{
		OperationResult IsValid(string name, string email, string address, string phone, string userType, string money);
	}
}