using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternController_1 : MonoBehaviour
{
    [SerializeField] private BossPattern defaultPattern;
    [SerializeField] private BossPattern circlePattern;
    [SerializeField] private BossPattern crossRotatePattern;
    [SerializeField] private BossPattern enemySpawnPattern;

    [SerializeField] private BossPattern cross1;
    [SerializeField] private BossPattern cross2;

    private List<BossPattern> curPatterns;

    public void CurrentBossPatternStop()
    {
        foreach (var pattern in curPatterns)
        {
            pattern.Stop();
        }
        curPatterns.Clear();
    }

    public void PlayCirclePattern()
    {
        curPatterns.Clear();
        circlePattern.Execute();
        curPatterns.Add(circlePattern);
    }

    public void PlayDefaultPattern()
    {
        curPatterns.Clear();
        defaultPattern.Execute();
        curPatterns.Add(defaultPattern);
    }

    public void PlayCrossRotatePattern()
    {
        curPatterns.Clear();
        crossRotatePattern.Execute();
        curPatterns.Add(crossRotatePattern);
    }

    public void PlayEnemySpawnPattern()
    {
        curPatterns.Clear();
        enemySpawnPattern.Execute();
        curPatterns.Add(enemySpawnPattern);
    }

    public void PlayDoubleCross()
    {
        curPatterns.Clear();
        cross1.Execute();
        cross2.Execute();
        curPatterns.Add(cross1);
        curPatterns.Add(cross2);
    }

    public void AddEventCircle(Action action) => circlePattern.OnComplated += action;
    public void AddEventDefault(Action action) => defaultPattern.OnComplated += action;
    public void AddEventCross(Action action) => crossRotatePattern.OnComplated += action;
    public void AddEventEnemySpawn(Action action) => enemySpawnPattern.OnComplated += action;
    public void AddEventDoubleCross(Action action) => cross1.OnComplated += action;
}