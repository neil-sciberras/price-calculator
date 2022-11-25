namespace backend.utilities
{
	public static class ArgCheck
	{
		public static T NotNull<T>(this T arg, string argName)
		{
			if (arg == null)
				throw new ArgumentNullException(paramName: argName);

			return arg;
		}
    }
}