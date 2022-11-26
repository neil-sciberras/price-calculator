namespace backend.domain
{
	public class ValidationResult
	{
		public ValidationResult(bool success, string message)
		{
			Success = success;
			Message = message;
		}

		public bool Success { get; }
		public string Message { get; }

		public ValidationResult CombineWith(ValidationResult other)
		{
			return new ValidationResult(
				success: Success && other.Success,
				message: string.Join(",", Message, other.Message));
		}
	}
}
