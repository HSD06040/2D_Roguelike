using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemStat;
    [SerializeField] private TMP_Text description;

    public void OpenToolTip(Accessories accessories)
    {
        if (accessories == null) return;

        gameObject.SetActive(true);

        itemIcon.sprite = accessories.icon;
        itemName.text = accessories.itemName;
        //itemStat.text = 
        description.text = accessories.description;
    }

    public void CloseToolTip()
    {
        gameObject.SetActive(false);
    }
}
