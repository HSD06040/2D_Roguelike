using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChanger : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset oldAsset;
    [SerializeField] private TMP_FontAsset newAsset;

    [ContextMenu("Change TMP_Text Font")]
    public void Change()
    {
        TMP_Text[] allTexts = FindObjectsOfType<TMP_Text>(true);

        int count = 0;

        foreach (TMP_Text text in allTexts)
        {
            if (text.font != newAsset)
            {
                text.font = newAsset;
                count++;
            }
        }

        Debug.Log($"{count}°³ÀÇ Font±³Ã¼ µÊ");
    }
}
