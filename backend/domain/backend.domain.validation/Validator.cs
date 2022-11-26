using Range = backend.domain.Limits.Range;

namespace backend.domain.validation
{
	public static class Validator
	{
		internal static ValidationResult Validate(Range range, decimal value, string validatedComponent)
		{
			var isValid = range.Includes(value);
			var message = !isValid ? $"{validatedComponent} {range.GetMessage()}" : string.Empty;

			return new ValidationResult(isValid, message);
		}
	}
}
