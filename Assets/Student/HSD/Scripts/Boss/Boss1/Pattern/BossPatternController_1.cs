using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPatternController_1 : MonoBehaviour
{
    [SerializeField] private BossPattern circlePattern;
    [SerializeField] private BossPattern xPattern;
    [SerializeField] private BossPattern crossPattern;
    [SerializeField] private BossPattern crossRotatePattern;

    public void PlayCirclePattern() => circlePattern.Execute();
    public void PlayXPattern() => xPattern.Execute();
    public void PlayCrossPattern() => crossPattern.Execute();
    public void PlayCrossRotatePattern() => crossRotatePattern.Execute();
}
