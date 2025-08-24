using System;
using SaveSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static Player Instance { get; private set; }
    
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        Instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Elevation.OnElevationChangeTriggerEnter += Elevation_OnElevationChangeTriggerEnter;
        Elevation.OnIncreasePlayerElevation += Elevation_OnIncreasePlayerElevation;
        Elevation.OnDecreasePlayerElevation += Elevation_OnDecreasePlayerElevation;
    }

    public PlayerData CreateSaveData()
    {
       return new PlayerData("John", transform.position); 
    }

    private void Elevation_OnElevationChangeTriggerEnter(object sender, EventArgs e)
    {
        LayerMask newExcludeMask = LayerMask.GetMask("Nothing");
        _rigidbody2D.excludeLayers = newExcludeMask;
    }

    private void Elevation_OnIncreasePlayerElevation(object sender, EventArgs e)
    {
        LayerMask newExcludeMask = LayerMask.GetMask("Collision");
        _rigidbody2D.excludeLayers = newExcludeMask;
    }
    
    private void Elevation_OnDecreasePlayerElevation(object sender, EventArgs e)
    {
        LayerMask newExcludeMask = LayerMask.GetMask("ElevatedCollision");
        _rigidbody2D.excludeLayers = newExcludeMask;
    }
    
}
