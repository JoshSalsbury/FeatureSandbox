using Utilities.GameTime;
using static Utilities.GameTime.TimeConversions;

namespace Extensions
{
    public static class TimeOnlyExtensions
    {
        
        /// <summary>
        /// Sets the <c>Second</c> component of a <c>TimeOnly</c> struct to zero.
        /// </summary>
        /// <param name="timeOnly">The <c>TimeOnly</c> struct to flatten.</param>
        /// <returns>Returns a new <c>TimeOnly</c> struct with the original <c>Hour</c> and <c>Minute</c> components and a <c>Second</c> component of zero.</returns>
        public static TimeOnly FlattenSeconds(this TimeOnly timeOnly)
        {
            return new TimeOnly(timeOnly.Hour.Value, timeOnly.Minute.Value);
        }

        /// <summary>
        /// Performs a "soft" equality check of two <c>TimeOnly</c> structs to determine if their <c>Hour</c> and <c>Minute</c> components are equal.
        /// </summary>
        /// <param name="left">The first <c>TimeOnly</c> struct to compare.</param>
        /// <param name="right">The second <c>TimeOnly</c> struct to compare.</param>
        /// <returns>Returns <c>true</c> if the <c>Hour</c> and <c>Minute</c> are equal. Returns <c>false</c> otherwise.</returns>
        public static bool IsHourAndMinuteEqual(this TimeOnly left, TimeOnly right)
        {
            return left.Hour == right.Hour && left.Minute == right.Minute;
        }

        /// <summary>
        /// Converts a <c>TimeOnly</c> to an integer value representing the number of seconds that has elapsed between 00:00:00 and the current time.
        /// </summary>
        /// <param name="timeOnly">The <c>TimeOnly</c> to convert into seconds.</param>
        /// <returns>An int representing the number of seconds that has elapsed between 00:00:00 and the current time.
        /// This value has a lower bound of <c>0</c> when the input is <c>(0,0,0)</c> and an upper bound of <c>86,399</c> when the input is <c>(23,59,59)</c>.</returns>
        public static int ToSeconds(this TimeOnly timeOnly)
        {
            return HoursToSeconds(timeOnly.Hour.Value) + MinutesToSeconds(timeOnly.Minute.Value) + timeOnly.Second.Value; 
        }
        
    }
}