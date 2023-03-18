using Backend.TechChallenge.Api.Controllers;

namespace Backend.TechChallenge.Api.Extensions
{
	public static class OperationResultExtension
	{
		public static Result ToResult(this OperationResult self)
		{
			return new Result
			{
				IsSuccess = self.IsSuccess,
				Message = self.Error,
			};
		}
	}
}