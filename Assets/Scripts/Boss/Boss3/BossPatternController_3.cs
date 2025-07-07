using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController_3 : MonoBehaviour
{
    public GameObject defaultAttackPrefab;

    [SerializeField] private BossPattern glissando;
    [SerializeField] private BossPattern cross;
    [SerializeField] private BossPattern pianoWhite;
    [SerializeField] private BossPattern pianoBlack;
    [SerializeField] private BossPattern line;

    private List<BossPattern> curPatterns = new();

    // === 패턴 실행 ===
    public void PlayCross()
    {
        curPatterns.Clear();
        cross.Execute();
        curPatterns.Add(cross);
    }

    public void PlayGlissando()
    {
        Debug.Log("Glissando가 실행되었습니다");
        curPatterns.Clear();
        glissando.Execute();
        curPatterns.Add(glissando);
    }

    public void PlayPianoWhite()
    {
        curPatterns.Clear();
        pianoWhite.Execute();
        curPatterns.Add(pianoWhite);
    }

    public void PlayPianoBlack()
    {
        curPatterns.Clear();
        pianoBlack.Execute();
        curPatterns.Add(pianoBlack);
    }

    public void PlayLine()
    {
        curPatterns.Clear();
        line.Execute();
        curPatterns.Add(line);
    }

    // === 이벤트 등록 ===
    public void AddLineEvent(Action action) => line.OnComplated += action;
    public void AddCrossEvent(Action action) => cross.OnComplated += action;
    public void AddGlissandoEvent(Action action) => glissando.OnComplated += action;
    public void AddPianoBlackEvent(Action action) => pianoBlack.OnComplated += action;
    public void AddPianoWhiteEvent(Action action) => pianoWhite.OnComplated += action;

    // === 패턴 정지 ===
    public void CurrentBossPatternStop()
    {
        foreach (var pattern in curPatterns)
        {
            pattern.Stop();
        }
        curPatterns.Clear();
    }
}
