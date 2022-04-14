using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Action OnTimerExpired;
    [SerializeField] private TimerPreviewer timerPreviewer;
    private int time;

    private void Start()
    {
        Level.OnEndLevel += StopAllCoroutines;
    }

    private void OnDestroy()
    {
        Level.OnEndLevel -= StopAllCoroutines;
    }

    public void Init(int _time)
    {
        time = _time;
        StartCoroutine(CountTimer());
    }

    private IEnumerator CountTimer()
    {
        while(time != 0)
        {
            timerPreviewer.UpdateTimerText(time);
            time--;
            yield return new WaitForSeconds(1);
        }
        OnTimerExpired?.Invoke();
    }
}
