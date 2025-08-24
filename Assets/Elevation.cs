using System;
using UnityEngine;

public class Elevation : MonoBehaviour
{

    public static event EventHandler OnElevationChangeTriggerEnter;
    public static event EventHandler OnIncreasePlayerElevation;
    public static event EventHandler OnDecreasePlayerElevation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnElevationChangeTriggerEnter?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayerAscendingElevation(collision))
        {
            OnIncreasePlayerElevation?.Invoke(this, EventArgs.Empty);
            return;
        }
        OnDecreasePlayerElevation?.Invoke(this, EventArgs.Empty);
    }

    private bool IsPlayerAscendingElevation(Collider2D collision)
    {
        return collision.transform.position.y > transform.position.y;
    }
    
}
