using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController_3 : MonoBehaviour
{
    public GameObject defaultAttackPrefab;
    [SerializeField] private BossPattern pattern;

    [SerializeField] private BossPattern glissando;
    [SerializeField] private BossPattern cross;
    [SerializeField] private BossPattern pianoWhite;
    [SerializeField] private BossPattern pianoBlack;
    [SerializeField] private BossPattern line;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            pattern.Execute();
    }

    public void PlayCross() => cross.Execute();
    public void PlayGlissando() => glissando.Execute();
    public void PlayPianoWhite() => pianoWhite.Execute();
    public void PlayPianoBlacck() => pianoBlack.Execute();
    public void PlayLine() => line.Execute();


    public void AddLineEvent(Action acion) => line.OnComplated += acion;
    public void AddCrossEvent(Action action) => cross.OnComplated += action;
    public void AddGlissandoEvent(Action action) => glissando.OnComplated += action;
    public void AddPianoBlackEvent(Action action) => pianoBlack.OnComplated += action;
    public void AddPianoWhiteEvent(Action action) => pianoWhite.OnComplated += action;
}
