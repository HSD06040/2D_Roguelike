using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesSlot : MonoBehaviour
{
    [SerializeField] private Image icon;    
    private Accessories accessories;    

    public void UpdateSlot(Accessories ac)
    {
        if (ac == null)
        {
            icon.sprite = null;
            icon.color = Color.clear;
            return;
        }

        accessories = ac;

        icon.sprite = accessories.icon;
        icon.color = Color.white;
    }
    public void UpdateSlot()
    {
        if (accessories == null)
        {
            icon.sprite = null;
            icon.color = Color.clear;
            return;
        }        

        icon.sprite = accessories.icon;
        icon.color = Color.white;
    }
}
