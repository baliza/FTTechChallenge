using Backend.TechChallenge.Core.Models;

namespace Backend.TechChallenge.Api.Services
{
	public class RequestValidator : IRequestValidator
	{
		public OperationResult IsValid(string name, string email, string address, string phone, string userType, string money)
		{
			//Validate if Name is null
			if (string.IsNullOrWhiteSpace(name))
				return new OperationResult(" The name is required");
			//Validate if Email is null
			if (string.IsNullOrWhiteSpace(email))
				return new OperationResult(" The email is required");
			//Validate if Address is null
			if (string.IsNullOrWhiteSpace(address))
				return new OperationResult(" The address is required");
			//Validate if Phone is null
			if (string.IsNullOrWhiteSpace(phone))
				return new OperationResult(" The phone is required");
			if (string.IsNullOrWhiteSpace(userType))
				return new OperationResult(" The userType is required");
			//Validate if money is decimal
			if (string.IsNullOrWhiteSpace(money)
				|| !decimal.TryParse(money, out _))
			{
				return new OperationResult(" The money has to be a decimal value");
			}

			return new OperationResult();
		}
	}
}