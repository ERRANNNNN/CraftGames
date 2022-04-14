using UnityEngine;
using TMPro;
using System;

public class TimerPreviewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public void UpdateTimerText(int time)
    {
        textMesh.text = GetTimeString(time);
    }

    private string GetTimeString(int time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
    }
}
