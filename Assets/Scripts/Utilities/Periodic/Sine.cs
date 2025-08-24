using UnityEngine;

namespace Utilities.Periodic
{
    public struct Sine
    {

        public float Amplitude;
        public float Period;
        public float PhaseShift;
        public float VerticalShift;

        public Sine(float amplitude, float period, float phaseShift, float verticalShift)
        {
            Amplitude = amplitude;
            Period = Mathf.PI * 2 / period;
            PhaseShift = phaseShift;
            VerticalShift = verticalShift;
        }

        public float CalculateValueAt(float t)
        {
            return Amplitude * Mathf.Sin(Period * t - PhaseShift) + VerticalShift;
        }

    }
}