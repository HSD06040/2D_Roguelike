using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : BaseUI
{


    void Start()
    {
        GetEvent("ReturnButton").Click += data => Manager.UI.ClosePopUp();
    }

}
