using UnityEngine;
using TMPro;

public class TimerUi : MonoBehaviour
{
    public TMP_Text timer;

    private void Start()
    {
        GameManager.Instance.OnTimeChanged += UpdateTimer;
    }
    private void UpdateTimer(float time)
    {
        if (time < 0) time = 0;
        int minute = Mathf.FloorToInt(time / 60);
        int second = Mathf.FloorToInt(time % 60);
        timer.text = $"{minute:00}:{second:00}";
    }
}
