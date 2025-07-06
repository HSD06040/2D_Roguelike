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

    public virtual void UpdateSlot(Accessories ac)
    {
        accessories = ac;

        if(ac == null)
        {
            icon.gameObject.SetActive(false);
            background.sprite = emptySlot;
            return;
        }

        background.sprite = filledSlot;
        icon.sprite = accessories.icon;
        icon.gameObject.SetActive(true);
    }
    public virtual void UpdateSlot()
    {
        if (accessories == null)
        {
            icon.gameObject.SetActive(false);
            background.sprite = emptySlot;

            icon.sprite = null;
            return;
        }

        icon.sprite = accessories.icon;
        icon.gameObject.SetActive(true);
    }
}
