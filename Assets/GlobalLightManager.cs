using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utilities.Periodic;

public class GlobalLightManager : MonoBehaviour
{
    
    private Light2D _light2D;
    private Sine _daylightIntensityByHour = new(0.45f, 1f, Mathf.PI / 2f, 0.55f);

    private void Awake()
    {
        _light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        TimeManager.Instance.OnTimeChanged += TimeManager_OnTimeChanged;
        _light2D.intensity = _daylightIntensityByHour.CalculateValueAt(TimeManager.Instance.CurrentTime.CalculateDayProgress());
    }

    private void TimeManager_OnTimeChanged(object sender, TimeManager.OnTimeChangedEventArgs e)
    {
        _light2D.intensity = _daylightIntensityByHour.CalculateValueAt(e.Time.CalculateDayProgress());
    }
    
}
