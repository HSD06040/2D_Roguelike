using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : AnimationUI_Base
{
    [SerializeField] private TMP_Text message;

    public void StartSetup(string _message)
    {
        message.text = _message;
        Open();
    }
}
