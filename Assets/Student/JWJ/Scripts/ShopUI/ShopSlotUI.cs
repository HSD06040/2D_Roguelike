using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemPriceText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button slotButton;

    private int slotIndex;
    private ShopView shopView;

    public void SetData(Item item, int index, ShopView view)
    {
        Debug.Log("SetData »£√‚µ : " + item.itemName);

        itemNameText.text = item.itemName;
        itemPriceText.text = item.Price.ToString("N0");
        itemIcon.sprite = item.icon;

        slotIndex = index;
        shopView = view;
        
        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(OnSlotClicked);
    }

    private void OnSlotClicked()
    {
        shopView.onSlotSelected(slotIndex);
    }

    

}
