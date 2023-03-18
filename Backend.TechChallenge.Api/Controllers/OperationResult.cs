namespace Backend.TechChallenge.Api.Controllers
{
	public class OperationResult
	{
		public OperationResult()
		{
			IsSuccess = true;
		}

		public OperationResult(string error)
		{
			IsSuccess = false;
			this.Error = error;
		}

		public bool IsSuccess { get; set; }
		public string Error { get; set; }
	}

	public class Result
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}