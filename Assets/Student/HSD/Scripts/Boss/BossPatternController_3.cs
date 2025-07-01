using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController_3 : MonoBehaviour
{
    [SerializeField] private BossPattern pattern;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            pattern.Execute();
    }
}
