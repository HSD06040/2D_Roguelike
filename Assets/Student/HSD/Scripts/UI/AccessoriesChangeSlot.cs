using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccessoriesChangeSlot : AccessoriesSlot, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private bool isNew;
    [SerializeField] private GameObject selectImage;
    private AccessoriesChangePanel accessoriesChangePanel;
    private int num;    

    public void Setup(AccessoriesChangePanel _accessoriesChangePanel, int _num = -1)
    {
        accessoriesChangePanel = _accessoriesChangePanel;
        num = _num;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (accessories == null) return;

        accessoriesChangePanel.toolTip.OpenToolTip(accessories);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        accessoriesChangePanel.toolTip.CloseToolTip();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isNew)
            accessoriesChangePanel.Select(num);
    }

    public void Select(bool active)
    {
        selectImage.gameObject.SetActive(active);
    }
}
