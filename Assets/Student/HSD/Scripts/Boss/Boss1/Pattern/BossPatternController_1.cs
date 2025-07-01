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

    public void PlayCirclePattern() => circlePattern.Execute();
    public void PlayDefaultPattern() => defaultPattern.Execute();    
    public void PlayCrossRotatePattern() => crossRotatePattern.Execute();
    public void PlayEnemySpawnPattern() => enemySpawnPattern.Execute();
    public void PlayDoubleCross()
    {
        cross1.Execute();
        cross2.Execute();
    }
    public void AddEventCircle(Action action) => circlePattern.OnComplated += action;
    public void AddEventDefault(Action action) => defaultPattern.OnComplated += action;
    public void AddEventCross(Action action) => crossRotatePattern.OnComplated += action;
    public void AddEventEnemySpawn(Action action) => enemySpawnPattern.OnComplated += action;
    public void AddEventDoubleCross(Action action) => cross1.OnComplated += action;
}
