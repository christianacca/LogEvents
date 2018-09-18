using System;

namespace CcAcca.LogEvents.Tests
{
    public static class TimeExts
    {
        public static TimeSpan Milliseconds(this int value)
        {
            return TimeSpan.FromMilliseconds(value);
        }
    }
}