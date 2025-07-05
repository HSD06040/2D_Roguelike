using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestUI : BaseUI
{
    private TMP_Text[] weaponTexts = new TMP_Text[4];

    private void Start()
    {
        for (int i = 0; i < weaponTexts.Length; i++)
        {
            weaponTexts[i] = GetUI<TMP_Text>($"WeaponText{i+1}");
        }
    }
}
