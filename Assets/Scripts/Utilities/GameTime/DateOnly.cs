using System;
using Extensions;
using JetBrains.Annotations;
using UnityEngine;
using static Utilities.GameTime.TimeConversions;

namespace Utilities.GameTime
{
    public partial struct DateOnly : IEquatable<DateOnly>
    {

        public int Year;
        public Season Season;
        public ModularInt Day;

        public DateOnly(int year, Season season, int day)
        {
            Day = new ModularInt(day, NUM_DAYS_PER_SEASON, out int overflow);
            int numExcessYears = Math.DivRem(overflow, NUM_DAYS_PER_YEAR, out int remainder);
            int numExcessSeasons = remainder / NUM_DAYS_PER_SEASON;
            Season = (Season)((int)season + numExcessSeasons).Mod(NUM_SEASONS_PER_YEAR, out int seasonOverflow);
            Year = year + numExcessYears + (seasonOverflow / NUM_SEASONS_PER_YEAR);
        }

        public bool IsBetween(DateOnly left, DateOnly right)
        {
            return this >= left && this <= right;
        }

        public bool IsSpring()
        {
            return Season == Season.Spring;
        }

        public bool IsSummer()
        {
            return Season == Season.Summer;
        }

        public bool IsFall()
        {
            return Season == Season.Fall;
        }

        public bool IsWinter()
        {
            return Season == Season.Winter;
        }

        public bool IsMonday()
        {
            return GetDayOfWeek() == DayOfWeek.Monday;
        }

        public bool IsTuesday()
        {
            return GetDayOfWeek() == DayOfWeek.Tuesday;
        }

        public bool IsWednesday()
        {
            return GetDayOfWeek() == DayOfWeek.Wednesday;
        }

        public bool IsThursday()
        {
            return GetDayOfWeek() == DayOfWeek.Thursday;
        }

        public bool IsFriday()
        {
            return GetDayOfWeek() == DayOfWeek.Friday;
        }

        public bool IsSaturday()
        {
            return GetDayOfWeek() == DayOfWeek.Saturday;
        }

        public bool IsSunday()
        {
            return GetDayOfWeek() == DayOfWeek.Sunday;
        }

        public bool IsWeekday()
        {
            return !IsWeekend();
        }

        public bool IsWeekend()
        {
            return IsSaturday() || IsSunday();
        }

        public DateOnly IncrementDay()
        {
            return new DateOnly(Year, Season, Day.Value + 1);
        }

        public DayOfWeek GetDayOfWeek()
        {
            int day = Day.Value.Mod(7);
            if (!Enum.IsDefined(typeof(DayOfWeek), day))
            {
                throw new ArgumentOutOfRangeException("day");
            }
            return (DayOfWeek)day;
        }

        public string GetDateAsString()
        {
            return $"{Day.Value+1} {Season}, {Year}";
        }

    }
    
    // Define equality and comparison operators
    public partial struct DateOnly
    {
        public override bool Equals([CanBeNull] object obj) => obj is DateOnly other && Equals(other);
        
        public bool Equals(DateOnly dateOnly) => Year == dateOnly.Year && Season == dateOnly.Season && Day == dateOnly.Day;
        
        public override int GetHashCode() => HashCode.Combine(Year, Season, Day);
        
        public static bool operator ==(DateOnly left, DateOnly right) => left.Equals(right);
        
        public static bool operator !=(DateOnly left, DateOnly right) => !(left == right);

        public static bool operator >(DateOnly left, DateOnly right)
        {
            if (left.Year != right.Year)
            {
                return left.Year > right.Year;
            }

            if (left.Season != right.Season)
            {
                return left.Season > right.Season;
            }

            if (left.Day != right.Day)
            {
                return left.Day > right.Day;
            }

            return false;
        }

        public static bool operator <(DateOnly left, DateOnly right) => !(left > right) && left != right;

        public static bool operator >=(DateOnly left, DateOnly right) => !(left < right);

        public static bool operator <=(DateOnly left, DateOnly right) => !(left > right);

        public static DateOnly operator +(DateOnly dateOnly, int days)
            => new(dateOnly.Year, dateOnly.Season, dateOnly.Day.Value + days);
        
        public static DateOnly operator +(int days, DateOnly dateOnly)
            => dateOnly + days;

        public static DateOnly operator +(DateOnly dateOnly, TimeOnly timeOnly)
            => dateOnly + timeOnly.Day;
        
        public static DateOnly operator +(TimeOnly timeOnly, DateOnly dateOnly)
            => dateOnly + timeOnly;

    }
    
}