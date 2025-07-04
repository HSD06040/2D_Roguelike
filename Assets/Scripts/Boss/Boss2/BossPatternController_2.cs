using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController_2 : MonoBehaviour
{
    [SerializeField] private BossPattern line;
    [SerializeField] private BossPattern crossRotate;
    [SerializeField] private BossPattern cross;
    [SerializeField] private BossPattern explosion;
    [SerializeField] private BossPattern teleport;
    [SerializeField] private BossPattern doubleShot;
    [SerializeField] private BossPattern laser;

    [Header("Test")]
    [SerializeField] private BossPattern test;

    private List<BossPattern> curPatterns = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            test.Execute();
    }

    // ==== 이벤트 등록 메서드들 ====
    public void AddLineEvent(Action action) => line.OnComplated += action;
    public void AddCrossRotateEvent(Action action) => crossRotate.OnComplated += action;
    public void AddCrossEvent(Action action) => cross.OnComplated += action;
    public void AddExplosionEvent(Action action) => explosion.OnComplated += action;
    public void AddTeleportEvent(Action action) => teleport.OnComplated += action;
    public void AddDoubleShotEvent(Action action) => doubleShot.OnComplated += action;
    public void AddLaserEvent(Action action) => laser.OnComplated += action;

    // ==== 실행 메서드들 ====
    public void PlayCrossRotatePattern()
    {
        curPatterns.Clear();
        line.Execute();
        crossRotate.Execute();
        curPatterns.Add(line);
        curPatterns.Add(crossRotate);
    }

    public void PlayCrossPattern()
    {
        curPatterns.Clear();
        line.Execute();
        cross.Execute();
        curPatterns.Add(line);
        curPatterns.Add(cross);
    }

    public void PlayLinePattern()
    {
        curPatterns.Clear();
        line.Execute();
        curPatterns.Add(line);
    }

    public void PlayExplosionPattern()
    {
        curPatterns.Clear();
        explosion.Execute();
        curPatterns.Add(explosion);
    }

    public void PlayTeleportPattern()
    {
        curPatterns.Clear();
        teleport.Execute();
        curPatterns.Add(teleport);
    }

    public void PlayDoubleShotPattern()
    {
        curPatterns.Clear();
        doubleShot.Execute();
        curPatterns.Add(doubleShot);
    }

    public void PlayLaserPattern()
    {
        curPatterns.Clear();
        laser.Execute();
        curPatterns.Add(laser);
    }

    public void CurrentBossPatternStop()
    {
        foreach (var pattern in curPatterns)
        {
            pattern.Stop();
        }
        curPatterns.Clear();
    }
}
