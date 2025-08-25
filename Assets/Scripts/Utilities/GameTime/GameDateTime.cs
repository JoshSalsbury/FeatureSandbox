using System;
using JetBrains.Annotations;

namespace Utilities.GameTime
{

    public partial struct GameDateTime : IEquatable<GameDateTime>
    {
        
        public DateOnly DateOnly;
        public TimeOnly TimeOnly;

        public GameDateTime(DateOnly dateOnly, TimeOnly timeOnly)
        {
            DateOnly = dateOnly;
            TimeOnly = timeOnly;
        }

        public GameDateTime(DateOnly dateOnly) : this(dateOnly, TimeOnly.Zero) {}

        public bool IsBetween(GameDateTime left, GameDateTime right)
        {
            return this >= left && this <= right;
        }

        public bool IsBetween(DateOnly left, DateOnly right)
        {
            return IsBetween(new GameDateTime(left), new GameDateTime(right));
        }

        public bool IsBetween(DateOnly left, GameDateTime right)
        {
            return IsBetween(new GameDateTime(left), right);
        }

        public bool IsBetween(GameDateTime left, DateOnly right)
        {
            return IsBetween(left, new GameDateTime(right));
        }
        
    }

    // Define equality and comparison operators
    public partial struct GameDateTime
    {
        
        public override bool Equals([CanBeNull] object obj)
            => obj is GameDateTime other && 
               Equals(other);
        
        public bool Equals(GameDateTime gameDateTime)
            => gameDateTime.DateOnly == DateOnly && 
               gameDateTime.TimeOnly.ClockEquals(TimeOnly);
        
        public override int GetHashCode()
            => HashCode.Combine(DateOnly, TimeOnly);
        
        public static bool operator ==(GameDateTime left, GameDateTime right)
            => left.Equals(right);
        
        public static bool operator !=(GameDateTime left, GameDateTime right)
            => !(left == right);

        public static bool operator <(GameDateTime left, GameDateTime right)
        {
            if (left.DateOnly != right.DateOnly)
            {
                return left.DateOnly < right.DateOnly;
            }
            return left.TimeOnly < right.TimeOnly;
        }
        
        public static bool operator >(GameDateTime left, GameDateTime right) 
            => !(left < right) && left != right;
        
        public static bool operator <=(GameDateTime left, GameDateTime right) 
            => !(left > right);
        
        public static bool operator >=(GameDateTime left, GameDateTime right) 
            => !(left < right);
        
    }
    
}
