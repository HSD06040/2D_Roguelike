using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dtest : MonoBehaviour
{
    [SerializeField] DialogueBoss DialogueBoss;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            DialogueBoss.DialogueFinal();
        }
    }
}
