using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : AnimationUI_Base
{
    [SerializeField] private ShopSlotUI[] slotUIs;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject background;
    //[SerializeField] private TMP_Text moneyText;

    private ShopPresenter shopPresenter;
    private Item[] currentItems;
    private int selectedIndex = -1;

    public void _Start(ShopPresenter presenter)
    {
        shopPresenter = presenter;

        buyButton.onClick.AddListener(OnBuyButtonClicked);
        //UpdateMoneyDisplay();
    }

    public void DisplayItems(Item[] items) //아이템들 슬롯에 표시
    {
        currentItems = items;

        slotUIs[0].SetData(items[0], 0, this);
        slotUIs[1].SetData(items[1], 1, this);
        slotUIs[2].SetData(items[2], 2, this);
    }

    public void onSlotSelected(int index)
    {
        selectedIndex = index;
    }

    private void OnBuyButtonClicked()
    {
        shopPresenter.TryToBuy(selectedIndex);
    }

    //public void UpdateMoneyDisplay()
    //{
    //    int money = shopPresenter.GetCurrentMoney();
    //    moneyText.text = "Balance" + money;
    //}

    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }

    public override void Open()
    {
        base.Open();
        background.SetActive(true);
    }
    public override void Close()
    {
        base.Close();
        background.SetActive(false);
    }
}
