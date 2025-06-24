using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    public Canvas WorldCanvas;
    public Canvas MainCanvas;

    private void Awake()
    {
        WorldCanvas = Instantiate<Canvas>(Resources.Load<Canvas>("UI/WorldCanvas"));
        WorldCanvas.transform.parent = transform;

        MainCanvas = Instantiate<Canvas>(Resources.Load<Canvas>("UI/MainCanvas"));
        MainCanvas.transform.parent = transform;
    }
}