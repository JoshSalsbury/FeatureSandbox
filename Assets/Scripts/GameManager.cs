using System;
using SaveSystem;
using UnityEngine;
using Utilities.Progress;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameInput gameInput;

    private void Awake()
    {
        Instance = this;
        gameInput.OnSaveRequested += GameInput_OnSaveRequested;
    }

    public Metadata CreateSaveData()
    {
        return new(
            "abcd",
            "MyWorld",
            DateTime.Now
        );
    }

    private void GameInput_OnSaveRequested(object sender, EventArgs e)
    {
        SaveManager.Save();
    }
    
}
