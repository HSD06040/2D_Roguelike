using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternController : MonoBehaviour
{
    [SerializeField] private BossPattern pattern;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            pattern.Execute();
    }
}
