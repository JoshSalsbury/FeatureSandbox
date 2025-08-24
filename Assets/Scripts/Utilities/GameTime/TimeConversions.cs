using System;

namespace Utilities.GameTime
{
    public static class TimeConversions
    {
        
        public const int NUM_SECONDS_PER_MINUTE = 60;
        public const int NUM_SECONDS_PER_HOUR = NUM_SECONDS_PER_MINUTE * NUM_MINUTES_PER_HOUR;
        public const int NUM_SECONDS_PER_DAY = NUM_MINUTES_PER_HOUR * NUM_HOURS_PER_DAY;
        public const int NUM_MINUTES_PER_HOUR = 60;
        public const int NUM_MINUTES_PER_DAY = NUM_HOURS_PER_DAY * NUM_MINUTES_PER_HOUR;
        public const int NUM_HOURS_PER_DAY = 24;

        public static int SecondsToHours(int seconds, out int remainder)
        {
            return Math.DivRem(seconds, NUM_SECONDS_PER_HOUR, out remainder);
        }

        public static int SecondsToMinutes(int seconds, out int remainder)
        {
            return Math.DivRem(seconds, NUM_SECONDS_PER_MINUTE, out remainder);
        }

        public static int HoursToSeconds(int hours)
        {
            return hours * NUM_SECONDS_PER_HOUR;
        }

        public static int MinutesToSeconds(int minutes)
        {
            return minutes * NUM_SECONDS_PER_MINUTE;
        }

        public static float MinutesToHours(int minutes)
        {
            return (float)minutes / NUM_MINUTES_PER_HOUR;
        }

        public static float SecondsToHours(int seconds)
        {
            return (float)seconds / NUM_SECONDS_PER_HOUR;
        }

    }
}