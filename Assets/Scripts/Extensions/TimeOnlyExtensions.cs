using Utilities.GameTime;
using static Utilities.GameTime.TimeConversions;

namespace Extensions
{
    public static class TimeOnlyExtensions
    {

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
        
    }
}