using System;
using Extensions;
using UnityEngine;
using Utilities.GameTime;
using static Utilities.GameTime.TimeConversions;

public class TimeManager : MonoBehaviour
{

    public event EventHandler<OnDateChangedEventArgs> OnDateChanged;

    public class OnDateChangedEventArgs : EventArgs
    {
        public DateOnly Date;
    }

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

    public DateOnly CurrentDate
    {
        get => _currentDate;
        private set
        {
            _currentDate = value;
            OnDateChanged?.Invoke(this, new OnDateChangedEventArgs { Date = value });
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
    private DateOnly _currentDate = new(2077, Season.Fall, 0);

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
        if (BackingTime.ClockEquals(CurrentTime))
        {
            return;
        }
        if (BackingTime.Day != CurrentTime.Day)
        {
            CurrentDate = CurrentDate.IncrementDay();
        }
        CurrentTime = BackingTime;
    }
    
}
