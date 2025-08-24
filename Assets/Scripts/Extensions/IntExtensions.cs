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
        /// <returns>Returns the simplest congruence of <c>value</c> mod <c>modulus</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int Mod(this int value, int modulus)
        {
            if (modulus <= 0)
            {
                throw new ArgumentException("Modulus cannot be zero or negative.", nameof(modulus));
            }
            int remainder = value % modulus;
            if (remainder < 0)
            {
                return remainder + modulus;
            }
            return remainder;
        }
    }
}