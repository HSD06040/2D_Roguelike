using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopUp : BaseUI
{
    private GameObject pressGameStartButton => GetUI("PressGameStartButton");
    private GameObject pressOptionButton => GetUI("PressOptionButton");
    private GameObject pressExitButton => GetUI("PressExitButton");

    void Start()
    {
        GetEvent("ReturnButton").Click += data =>
        {
            Manager.UI.ClosePopUp();
            pressExitButton.SetActive(true);
            pressGameStartButton.SetActive(true);
            pressOptionButton.SetActive(true);
        };
    }

}
