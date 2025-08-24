using System;
using Extensions;
using UnityEngine;
using Utilities.GameTime;
using static Utilities.GameTime.TimeConversions;

public class TimeManager : MonoBehaviour
{

    public event EventHandler<OnTimeChangedEventArgs> OnTimeChanged;

    public class OnTimeChangedEventArgs : EventArgs
    {
        public TimeOnly Time;
    }

    private event EventHandler OnEpochTimeChanged;
    
    public static TimeManager Instance;

    public TimeOnly CurrentTime
    {
        get => _currentTime;
        private set
        {
            _currentTime = value;
            OnTimeChanged?.Invoke(this, new OnTimeChangedEventArgs { Time = value });
        }
    }
    
    [SerializeField] private bool is24HourTime;
    [SerializeField] private int gameDayDurationInRealWorldMinutes;
    [SerializeField] private float playerTimeScalePreference = 1f;

    private float EpochTime
    {
        get => _epochTime;
        set
        {
            _epochTime = value;
            OnEpochTimeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    private float _epochTime;
    private TimeOnly _currentTime = new(0, 0);

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnEpochTimeChanged += TimeManager_OnEpochTimeChanged;
    }

    private void Update()
    {
        EpochTime += GetTimeScale() * Time.deltaTime;
    }

    public bool Is24HourTime()
    {
        return is24HourTime;
    }

    private float GetTimeScale()
    {
        return playerTimeScalePreference * ((float)NUM_MINUTES_PER_DAY / gameDayDurationInRealWorldMinutes);
    }

    private void TimeManager_OnEpochTimeChanged(object sender, EventArgs e)
    {
        TimeOnly newTime = new(_epochTime);
        if (!newTime.IsHourAndMinuteEqual(_currentTime))
        {
            CurrentTime = newTime;
        }
    }
    
}
