namespace Backend.TechChallenge.Core.Models
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
			this.Message = error;
		}

		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}