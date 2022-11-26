using System.Diagnostics.CodeAnalysis;

namespace backend.api.contracts
{
	/// <summary>
	/// Represents the result of validating the price request against the partner's criteria
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class ValidationResult
	{
		public ValidationResult(bool success, string? message)
		{
			Success = success;
			Message = message;
		}

		public bool Success { get; }
		public string? Message { get; }
	}
}
