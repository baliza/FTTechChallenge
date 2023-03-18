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

	public class OperationResult<T>
	{
		public OperationResult(T data)
		{
			IsSuccess = true;
			Data = data;
			Message = "";
		}

		public OperationResult(string error)
		{
			IsSuccess = false;
			Message = error;
		}

		public bool IsSuccess { get; private set; }
		public T Data { get; private set; }
		public string Message { get; set; }
	}
}