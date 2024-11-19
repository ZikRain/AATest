

using System;

namespace Shared.Helpers
{
    public static class UnixHelper
    {
        public static long ToUnix(this DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeSeconds();
        }

        public static DateTime ToDateTimeUnix(this long unix)
        {
            return (new DateTime(1970, 1, 1)).AddSeconds(unix);
        }
    }
}
