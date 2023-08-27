namespace SubsurfaceStudios.Utilities.Math {
	public static class MathUtils {
		/// <summary>
		/// Returns the first power of two that is greater than or equal to the input.
		/// </summary>
		public static ulong NextPowerOfTwo(ulong input) {
			input--;

			input |= input >> 1;
			input |= input >> 2;
			input |= input >> 4;
			input |= input >> 8;
			input |= input >> 16;
			input |= input >> 32;

			input++;

			return input;
		}
	}
}