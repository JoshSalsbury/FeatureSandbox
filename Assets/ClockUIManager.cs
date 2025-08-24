using TMPro;
using UnityEngine;

public class ClockUIManager : MonoBehaviour
{
    
    private TextMeshProUGUI _clockTextMeshProUGUI;

    private void Awake()
    {
        _clockTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        TimeManager.Instance.OnTimeChanged += TimeManager_OnTimeChanged;
        _clockTextMeshProUGUI.text = TimeManager.Instance.CurrentTime.GetTimeAsString(TimeManager.Instance.Is24HourTime());
    }

    private void TimeManager_OnTimeChanged(object sender, TimeManager.OnTimeChangedEventArgs e)
    {
        _clockTextMeshProUGUI.text = e.Time.GetTimeAsString(TimeManager.Instance.Is24HourTime());
    }
    
}
