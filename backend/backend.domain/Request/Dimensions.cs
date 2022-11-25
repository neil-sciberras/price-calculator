namespace backend.domain.Request
{
	public class Dimensions
	{
		private readonly decimal _width;
		private readonly decimal _height;
		private readonly decimal _depth;

		public Dimensions(decimal width, decimal height, decimal depth)
		{
			_width = width;
			_height = height;
			_depth = depth;
		}

		public decimal Volume => _width * _height * _depth;
	}
}
