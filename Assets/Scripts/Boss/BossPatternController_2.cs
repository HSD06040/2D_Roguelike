using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController_2 : MonoBehaviour
{
    [SerializeField] private BossPattern linePattern;
    [SerializeField] private BossPattern crossRotatePattern;
    [SerializeField] private BossPattern crossPattern;
    [SerializeField] private BossPattern test;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            test.Execute();
    }

    public void AddCrossRotateEvent(Action action) => crossRotatePattern.OnComplated += action;
    public void AddCrossEvent(Action action) => crossRotatePattern.OnComplated += action;

    public void PlayCrossRotatePattern()
    {
        linePattern.Execute();
        crossRotatePattern.Execute();
    }

    public void PlayCrossPattern()
    {
        linePattern.Execute();
        crossPattern.Execute();
    }
}
