using System;
using Extensions;
using JetBrains.Annotations;
using UnityEngine;
using static Utilities.GameTime.TimeConversions;

namespace Utilities.GameTime
{
    // Test
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter,
    }

    /// <summary>
    /// Struct that implements the <c>ModularInt</c> struct to create a clock-like 24-hour time value with <c>Hour</c>, <c>Minute</c>, and <c>Second</c> components.
    /// </summary>
    public struct TimeOnly : IEquatable<TimeOnly>
    {

        public ModularInt Hour;
        public ModularInt Minute;
        public ModularInt Second;

        public TimeOnly(int hour, int minute, int second = 0)
        {
            Hour = new ModularInt(hour, 24);
            Minute = new ModularInt(minute, 60);
            Second = new ModularInt(second, 60);
        }

        public TimeOnly(int seconds) : this(SecondsToHours(seconds, out int remainder), SecondsToMinutes(remainder, out int second), second)
        {
        }
        
        public TimeOnly(float seconds) : this(Mathf.FloorToInt(seconds))
        {}

        public float GetPreciseHours()
        {
            return Hour.Value + MinutesToHours(Minute.Value) + SecondsToHours(Second.Value);
        }
        
        public string GetTimeAsString(bool is24HourTime = false)
        {
            if (is24HourTime)
            {
                return $"{Hour.Value:00}:{Minute.Value:00}";
            }
            return $"{Convert24HourTo12Hour():00}:{Minute.Value:00} {Get12HourTimeSuffix()}";
        }

        private void AddHours(int hours)
        {
            Hour.Add(hours);
        }

        private void AddMinutes(int minutes)
        {
            Minute.Add(minutes, out int overflow);
            AddHours(overflow);
        }

        private void AddSeconds(int seconds)
        {
            Second.Add(seconds, out int overflow);
            AddMinutes(overflow);
        }

        private int Convert24HourTo12Hour()
        {
            if (Hour.Value == 0)
            {
                return Hour.Value + 12;
            }
            if (Hour.Value > 12)
            {
                return Hour.Value - 12;
            }
            return Hour.Value;
        }
        
        private bool IsMorning()
        {
            return Hour.Value < 12;
        }

        private string Get12HourTimeSuffix()
        {
            return IsMorning() ? "AM" : "PM";
        }
        
        // Override operators
        public override bool Equals([CanBeNull] object obj) 
            => obj is TimeOnly other && 
               Equals(other);

        public bool Equals(TimeOnly timeOnly) 
            => Hour == timeOnly.Hour && 
               Minute == timeOnly.Minute && 
               Second == timeOnly.Second;
        
        public override int GetHashCode() 
            => (Hour, Minute, Second).GetHashCode();
        
        public static bool operator ==(TimeOnly left, TimeOnly right) 
            => left.Equals(right);
        
        public static bool operator !=(TimeOnly left, TimeOnly right) 
            => !(left == right);
        
        public static bool operator <(TimeOnly left, TimeOnly right) 
            => left.ToSeconds() < right.ToSeconds();
        
        public static bool operator >(TimeOnly left, TimeOnly right) 
            => left.ToSeconds() > right.ToSeconds();
        
        public static bool operator <=(TimeOnly left, TimeOnly right) 
            => left.ToSeconds() <= right.ToSeconds();
        
        public static bool operator >=(TimeOnly left, TimeOnly right) 
            => left.ToSeconds() >= right.ToSeconds();
        
        public static TimeOnly operator +(TimeOnly left, TimeOnly right) 
            => new(left.ToSeconds() + right.ToSeconds());
        
        public static TimeOnly operator +(TimeOnly timeOnly, int seconds)
            => new(timeOnly.ToSeconds() + seconds);
        
        public static TimeOnly operator +(int seconds, TimeOnly timeOnly)
            => timeOnly + seconds;

    }

    public struct DateOnly : IEquatable<DateOnly>
    {

        public int Year;
        public Season Season;
        public ModularInt Day;

        public DateOnly(int year, Season season, int day)
        {
            Year = year;
            Season = season;
            Day = new ModularInt(day, 28);
        }
        
        public override bool Equals([CanBeNull] object obj) => obj is DateOnly other && Equals(other);
        
        public bool Equals(DateOnly dateOnly) => Year == dateOnly.Year && Season == dateOnly.Season && Day == dateOnly.Day;
        
        public override int GetHashCode() => HashCode.Combine(Year, Season, Day);
        
        public static bool operator ==(DateOnly left, DateOnly right) => left.Equals(right);
        
        public static bool operator !=(DateOnly left, DateOnly right) => !(left == right);
        
    }

    public struct GameDateTime
    {
        
        public DateOnly DateOnly;
        public TimeOnly TimeOnly;

        public GameDateTime(DateOnly dateOnly, TimeOnly timeOnly)
        {
            DateOnly = dateOnly;
            TimeOnly = timeOnly;
        }
        
    }
    
}
