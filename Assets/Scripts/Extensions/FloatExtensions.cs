using UnityEngine;

public static class FloatExtensions
{

    public static int RoundToNearest(this float value, int nearest = 1)
    {
        
        if (nearest == 0)
        {
            nearest = 1;
        }
        
        return Mathf.RoundToInt(value / nearest) * nearest;
        
    }
    
}
