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

    private event EventHandler OnBackingTimeChanged;
    
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

    private TimeOnly BackingTime
    {
        get => _backingTime;
        set
        {
            _backingTime = value;
            OnBackingTimeChanged?.Invoke(this, new OnTimeChangedEventArgs { Time = value });
        }
    }

    private TimeOnly _backingTime = new(0);
    private TimeOnly _currentTime = new(0);

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnBackingTimeChanged += TimeManager_OnBackingTimeChanged;
    }

    private void Update()
    {
        BackingTime += GetTimeScale() * Time.deltaTime;
    }

    public bool Is24HourTime()
    {
        return is24HourTime;
    }

    private float GetTimeScale()
    {
        return playerTimeScalePreference * ((float)NUM_MINUTES_PER_DAY / gameDayDurationInRealWorldMinutes);
    }

    private void TimeManager_OnBackingTimeChanged(object sender, EventArgs e)
    {
        if (!BackingTime.IsHourAndMinuteEqual(CurrentTime))
        {
            CurrentTime = BackingTime;
        }
    }
    
}
