namespace backend.domain
{
	public class ValidationResult
	{
		public ValidationResult(bool success, string? message)
		{
			Success = success;
			Message = message;
		}

		public bool Success { get; }
		public string? Message { get; }

		public ValidationResult CombineWith(ValidationResult other)
		{
			var success = Success && other.Success;

			var message = Success switch
			{
				true when other.Success => null,
				true when !other.Success => other.Message,
				false when other.Success => Message,
				_ => string.Join(", ", Message, other.Message)
			};

			return new ValidationResult(
				success: success,
				message: message);
		}
	}
}
