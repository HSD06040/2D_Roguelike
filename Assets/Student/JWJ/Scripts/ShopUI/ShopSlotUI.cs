using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemPriceText;
    [SerializeField] private Button slotButton;


    private int slotIndex;
    private ShopView shopView;

    public void SetData(KeyValuePair<string, int> item, int index, ShopView view)
    {
        Debug.Log("SetData »£√‚µ : " + item.Key);

        itemNameText.text = item.Key;
        itemPriceText.text = item.Value.ToString();

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
