using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Accessories_InfoSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private int idx;
    [SerializeField] private ToolTip toolTip;
    private Accessories ac;    

    private void OnEnable()
    {
        ac = Manager.Data.PlayerStatus.PlayerAccessories[idx];

        if(ac == null)
        {
            image.sprite = null;
            image.color = Color.clear;
        }
        else
        {
            image.sprite = ac.icon;
            image.color = Color.white;
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ac != null)
            toolTip.OpenToolTip(ac);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(toolTip.gameObject.activeSelf)
            toolTip.CloseToolTip();
    }
}
