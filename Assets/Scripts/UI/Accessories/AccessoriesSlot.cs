using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesSlot : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] protected Image icon;
    [SerializeField] private Sprite filledSlot;
    [SerializeField] private Sprite emptySlot;


    [SerializeField] protected Accessories accessories;    

    public void UpdateSlot(Accessories ac)
    {
        accessories = ac;
        //if (ac == null)
        //{
        //    icon.sprite = null;
        //    icon.color = Color.clear;
        //    return;
        //}

        if(ac == null)
        {
            icon.gameObject.SetActive(false);
            background.sprite = emptySlot;
            return;
        }
        background.sprite = filledSlot;
        icon.sprite = accessories.icon;
        icon.gameObject.SetActive(true);
        //icon.color = Color.white;
    }
    public void UpdateSlot()
    {
        if (accessories == null)
        {

            icon.gameObject.SetActive(false);
            background.sprite = emptySlot;
            //return;

            icon.sprite = null;
            //icon.color = Color.clear;
            return;
        }

        //background.sprite = filledSlot;
        icon.sprite = accessories.icon;
        icon.gameObject.SetActive(true);
        

        //icon.color = Color.white;
    }
}
