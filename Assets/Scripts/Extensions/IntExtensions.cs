using System;

namespace Extensions
{
    
    public static class IntExtensions
    {
        /// <summary>
        /// Modular arithmetic function that determines the simplest congruence of a value mod m.
        /// </summary>
        /// <param name="value">The integer to determine the simplest congruence of.</param>
        /// <param name="modulus">The integer that determines the congruence relation.</param>
        /// <param name="overflow">The "excess" value passed to the function. Defined as the distance between the passed value and its simplest congruence relative to the modulus.
        /// For example, the simplest congruence of 13 mod 10 is 3, so the overflow is 13 -3 = 10. For a negative value, -3 mod 10, its simplest congruence is 7. Thus, the
        /// overflow is 7 - (-3) = 10. Note: the overflow will always be a multiple of the modulus.</param>
        /// <returns>Returns the simplest congruence of <c>value</c> mod <c>modulus</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int Mod(this int value, int modulus, out int overflow)
        {
            if (modulus <= 0)
            {
                throw new ArgumentException("Modulus cannot be zero or negative.", nameof(modulus));
            }
            int remainder = value % modulus;
            if (remainder < 0)
            {
                int output = remainder + modulus;
                overflow = Math.Abs(value - output);
                return remainder + modulus;
            }
            overflow = Math.Abs(remainder - value);
            return remainder;
        }

        public static int Mod(this int value, int modulus)
        {
            return Mod(value, modulus, out _);
        }
        
    }
}