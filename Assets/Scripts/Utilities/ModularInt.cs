using System;
using Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utilities
{
    
    /// <summary>
    /// Struct that implements modular arithmetic on an integer. The <c>Value</c> will always return as the simplest form modulo the <c>modulus</c>. The <c>ModularInt</c> struct counts from <c>0</c>,
    /// so the <c>modulus</c> represents the maximum value plus one. For instance, in a <c>mod 10</c> <c>ModularInt</c>, the simplest form values go from <c>0</c> to <c>9</c>, and then <c>10</c> will get simplified
    /// to its simplest congruence, <c>0</c>.
    /// </summary>
    public struct ModularInt : IEquatable<ModularInt>
    {

        public int Value
        {
            get => _value;
            private set => _value = value.Mod(_modulus);
        }

        private int _value;
        private readonly int _modulus;

        public ModularInt(int value, int modulus = 1)
        {
            _value = value.Mod(modulus);
            _modulus = modulus;
        }

        public void Add(int value, out int overflow)
        {
            overflow = value - (value % _modulus);
            Value += value;
        }
        
        public void Add(int value)
        {
            Add(value, out int overflow);
        }
            
        // Override and define equality operators
        public override bool Equals([CanBeNull] object obj) => obj is ModularInt other && Equals(other);
            
        public bool Equals(ModularInt modularInt) => Value == modularInt.Value;
            
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ModularInt left, ModularInt right)
        {
            AssertModulusIsEqual(left, right);
            return left.Equals(right);
        }
            
        public static bool operator !=(ModularInt left, ModularInt right) => !(left == right);

        public static ModularInt operator +(ModularInt modularInt, int value) => new(modularInt.Value + value, modularInt._modulus);
        
        public static ModularInt operator +(int value, ModularInt modularInt) => modularInt + value;

        public static bool operator >(ModularInt left, ModularInt right)
        {
            AssertModulusIsEqual(left, right);
            return left.Value > right.Value;
        }
        
        public static bool operator <(ModularInt left, ModularInt right)
        {
            AssertModulusIsEqual(left, right);
            return left.Value < right.Value;
        }
        
        public static bool operator >=(ModularInt left, ModularInt right)
        {
            AssertModulusIsEqual(left, right);
            return left.Value > right.Value;
        }
        
        public static bool operator <=(ModularInt left, ModularInt right)
        {
            AssertModulusIsEqual(left, right);
            return left.Value < right.Value;
        }

        private static void AssertModulusIsEqual(ModularInt left, ModularInt right)
        {
            Debug.Assert(
                left._modulus == right._modulus,
                "ModularInt must have the same modulus to perform arithmetic and comparison operations."
            );
        }

    }
}
