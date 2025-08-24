using System;
using JetBrains.Annotations;
using UnityEngine;
using static Utilities.GameTime.TimeConversions;

namespace Utilities.GameTime
{
    public partial struct TimeOnly : IEquatable<TimeOnly>
    {
        
        private float EpochTime { get; set; }

        public int Day => Mathf.FloorToInt(EpochTime / NUM_SECONDS_PER_DAY);

        public int Hour => Mathf.FloorToInt((EpochTime - DaysToSeconds(Day)) / NUM_SECONDS_PER_HOUR);

        public int Minute => Mathf.FloorToInt((EpochTime - ToSeconds(Day, Hour, 0, 0)) / NUM_SECONDS_PER_MINUTE);

        public int Second => Mathf.FloorToInt(EpochTime - ToSeconds(Day, Hour, Minute, 0));

        public TimeOnly(int days, int hours, int minutes, int seconds)
        {
            EpochTime = ToSeconds(days, hours, minutes, seconds);
        }
        
        public TimeOnly(int hours, int minutes, int seconds) : this(0, hours, minutes, seconds) {}

        public TimeOnly(int hours, int minutes) : this(0, hours, minutes, 0) {}

        public TimeOnly(int seconds) : this(0, 0, 0, seconds) {}

        public TimeOnly(float seconds)
        {
            EpochTime = seconds;
        }

        public TimeOnly FlattenDays()
        {
            return new TimeOnly(EpochTime - Day * NUM_SECONDS_PER_DAY);
        }

        public bool IsBetween(TimeOnly left, TimeOnly right)
        {
            return this >= left && this <= right;
        }

        public bool ClockEquals(TimeOnly timeOnly, bool includeSeconds = false)
        {
            bool output = timeOnly.Hour == Hour && timeOnly.Minute == Minute;
            if (includeSeconds)
            {
                output = output && timeOnly.Second == Second;
            }
            return output;
        }

        public string GetTimeAsString(bool is24HourTime = false)
        {
            if (is24HourTime)
            {
                return $"{Hour:00}:{Minute:00}";
            }
            return $"{Convert24HourTo12Hour(Hour):00}:{Minute:00} {Get12HourTimeSuffix()}";
        }

        public float CalculateDayProgress()
        {
            return (float)ToSeconds(0, Hour, Minute, Second) / NUM_SECONDS_PER_DAY;
        }

        public int ToSeconds()
        {
            return ToSeconds(Day, Hour, Minute, Second);
        }

        private static int ToSeconds(int days, int hours, int minutes, int seconds)
        {
            return DaysToSeconds(days) + HoursToSeconds(hours) + MinutesToSeconds(minutes) + seconds;
        }

        private bool IsMorning()
        {
            return Hour < 12;
        }

        private string Get12HourTimeSuffix()
        {
            return IsMorning() ? "AM" : "PM";
        }
        
    }
    
    public partial struct TimeOnly
    {
        public static TimeOnly Zero = new(0);
    }
    
    // Override comparison and arithmetic operators
    public partial struct TimeOnly
    {
        
        public override bool Equals([CanBeNull] object obj) 
            => obj is TimeOnly other && 
               Equals(other);

        public bool Equals(TimeOnly timeOnly)
            => timeOnly.ToSeconds() == ToSeconds();
        
        public override int GetHashCode() 
            => ToSeconds().GetHashCode();
        
        public static bool operator ==(TimeOnly left, TimeOnly right) 
            => left.Equals(right);
        
        public static bool operator !=(TimeOnly left, TimeOnly right) 
            => !(left == right);
        
        public static bool operator <(TimeOnly left, TimeOnly right) 
            => left.ToSeconds() < right.ToSeconds();
        
        public static bool operator >(TimeOnly left, TimeOnly right) 
            => !(left < right) && left != right;
        
        public static bool operator <=(TimeOnly left, TimeOnly right) 
            => !(left > right);
        
        public static bool operator >=(TimeOnly left, TimeOnly right) 
            => !(left < right);
        
        public static TimeOnly operator +(TimeOnly left, TimeOnly right) 
            => new(left.EpochTime + right.EpochTime);
        
        public static TimeOnly operator +(TimeOnly timeOnly, int seconds)
            => new(timeOnly.EpochTime + seconds);
        
        public static TimeOnly operator +(int seconds, TimeOnly timeOnly)
            => timeOnly + seconds;
        
        public static TimeOnly operator +(TimeOnly timeOnly, float seconds)
            => new(timeOnly.EpochTime + seconds);
        
        public static TimeOnly operator +(float seconds, TimeOnly timeOnly)
            => timeOnly + seconds;
        
        public static TimeOnly operator -(TimeOnly left, TimeOnly right)
            => new(left.ToSeconds() - right.ToSeconds());

        public static TimeOnly operator -(TimeOnly timeOnly, int seconds)
            => new(timeOnly.ToSeconds() - seconds);
        
        public static TimeOnly operator -(int seconds, TimeOnly timeOnly)
            => new(seconds - timeOnly.ToSeconds());

    }
    
}