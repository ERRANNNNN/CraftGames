using UnityEngine;
using System;

public class Level : MonoBehaviour
{
    private int servicedVisitors = 0;
    [SerializeField] private VisitorsSpawner visitorsSpawner;
    [SerializeField] private Timer timer;
    [SerializeField] private VisitorsCountPreviewer visitorsCountPreviewer;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Booster booster;
    private LevelSettings levelSettings;
    public static Action OnEndLevel;

    private void Start()
    {
        Timer.OnTimerExpired += Lose;
        RestartButton.OnRestart += Restart;
        Init();
    }
    private void OnDestroy()
    {
        Visitor.OnVisitorServed -= UpdateServicedVisitors;
        RestartButton.OnRestart -= Restart;
        Timer.OnTimerExpired -= Lose;
    }

    private void Init()
    {
        Visitor.OnVisitorServed += UpdateServicedVisitors;
        servicedVisitors = 0;
        levelSettings = new LevelSettings();
        visitorsSpawner.Init(levelSettings);
        visitorsCountPreviewer.Init(levelSettings.GetVisitorsSettings.visitorsCount);
        timer.Init(levelSettings.LevelTime);
        booster.Init(levelSettings.BoosterCount);
    }

    private void UpdateServicedVisitors(Visitor visitor)
    {
        servicedVisitors++;
        visitorsCountPreviewer.UpdateRemainingVisitorsCount(levelSettings.GetVisitorsSettings.visitorsCount - servicedVisitors);
        if (servicedVisitors == levelSettings.GetVisitorsSettings.visitorsCount)
        {
            Win();
        }
    }

    private void Lose()
    {
        losePanel.SetActive(true);
        OnEndLevel?.Invoke();
    }

    private void Win()
    {
        winPanel.SetActive(true);
        OnEndLevel?.Invoke();
    }

    private void Restart()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        Visitor.OnExit = null;
        Visitor.OnVisitorServed = null;
        Booster.OnBoosterUsed = null;
        Init();
    }
}
