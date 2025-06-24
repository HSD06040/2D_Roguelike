using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private ShopSlotUI[] slotUIs;
    private ShopPresenter shopPresenter;

    public void _Start(ShopPresenter presenter)
    {
        this.shopPresenter = presenter;
    }

    public void DisplayItems(List<KeyValuePair<string, int>> displayItems) //아이템들 슬롯에 표시
    {
        //slotUIs[0].SetData(displayItems[0], shopPresenter.TryToBuy);

    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }
}
