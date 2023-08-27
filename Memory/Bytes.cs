using System.Runtime.InteropServices;
using System;

namespace SubsurfaceStudios.Utilities.Memory.Unsafe {
    /// <summary>
    /// A collection of relatively-unsafe functions for the manipulation of memory structures.
    /// </summary>
    /// <remarks>
    /// These functions are not particularly safe. Use with caution.
    /// </remarks>
	public static class Bytes {
        /// <summary>
        /// Copies the memory representation of a struct into a byte array of the same size.
        /// </summary>
        /// <remarks>
        /// This function is dependent on the layout of the structure specified.
        /// Therefore, this should be used with caution.
        /// </remarks>
        /// <param name="value">The struct to copy data from.</param>
        /// <typeparam name="T">The type of struct to convert from.</typeparam>
        public static TaggedUnion<byte[], Exception> Of<T>(in T value) where T: struct {
			int size = Marshal.SizeOf(value);
            byte[] bytes = new byte[size];

			IntPtr ptr = IntPtr.Zero;
			try {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, bytes, 0, size);
            } catch (Exception ex) {
				return new(ex);
			} finally {
				Marshal.FreeHGlobal(ptr);
			}

			return new(bytes);
		}

        /// <summary>
        /// Converts a properly-sized array of bytes into a struct directly.
        /// </summary>
        /// <remarks>
        /// This function is dependent on the layout of the structure specified.
        /// Therefore, this should be used with caution.
        /// </remarks>
        /// <param name="bytes">The array of bytes to copy data from.</param>
        /// <typeparam name="T">The type of struct to convert to.</typeparam>
        /// <returns>A tagged union containing either the converted struct or an error the function encountered.</returns>
		public static TaggedUnion<T, Exception> To<T>(this byte[] bytes) where T: struct {
            T value = default;

            int size = Marshal.SizeOf(value);
            if (size != bytes.Length)
                return new(new ArgumentException($"The size of the struct ({size}) does not match the size of the byte array ({bytes.Length})."));

            IntPtr ptr = IntPtr.Zero;

            try {
                ptr = Marshal.AllocHGlobal(size);

                Marshal.Copy(bytes, 0, ptr, size);

                value = Marshal.PtrToStructure<T>(ptr);
            } catch (Exception ex) {
                return new(ex);
            } finally {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }

			return new(value);
		}
	}
}
