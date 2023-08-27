namespace SubsurfaceStudios.Utilities.Time {
    using System;

    public static class TimeUtils {
        public static long ToUnixTimestamp(this DateTime value) => (long)value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        public static long CurrentUnixTimestamp() => DateTime.Now.ToUnixTimestamp();
    }
}