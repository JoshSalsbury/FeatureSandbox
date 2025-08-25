using TMPro;
using UnityEngine;

public class DateUIManager : MonoBehaviour
{
    
    private TextMeshProUGUI _dateTextMeshProUGUI;

    private void Awake()
    {
        _dateTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        TimeManager.Instance.OnDateChanged += TimeManager_OnDateChanged;
        _dateTextMeshProUGUI.text = TimeManager.Instance.CurrentDate.GetDateAsString();
    }

    private void TimeManager_OnDateChanged(object sender, TimeManager.OnDateChangedEventArgs e)
    {
        _dateTextMeshProUGUI.text = TimeManager.Instance.CurrentDate.GetDateAsString();
    }
    
}
