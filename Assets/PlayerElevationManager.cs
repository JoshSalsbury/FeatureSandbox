using System;
using UnityEngine;

public class PlayerElevationManager : MonoBehaviour
{
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Elevation.OnIncreasePlayerElevation += Elevation_OnIncreasePlayerElevation;
        Elevation.OnDecreasePlayerElevation += Elevation_OnDecreasePlayerElevation;
    }
    
    private void Elevation_OnIncreasePlayerElevation(object sender, EventArgs e)
    {
        _spriteRenderer.sortingLayerName = "ElevatedPlayer";
    }
    
    private void Elevation_OnDecreasePlayerElevation(object sender, EventArgs e)
    {
        _spriteRenderer.sortingLayerName = "Player";
    }
    
}
