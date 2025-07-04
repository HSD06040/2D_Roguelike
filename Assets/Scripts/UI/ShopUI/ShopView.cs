using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : AnimationUI_Base
{
    [SerializeField] private ShopSlotUI[] slotUIs;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject background;
    [SerializeField] private Button closeButton;
    //[SerializeField] private TMP_Text moneyText;

    private ShopPresenter shopPresenter;
    private Item[] currentItems;
    private int selectedIndex = -1;
    

    public event Action CloseButtonClicked;

    public void _Start(ShopPresenter presenter)
    {
       
        shopPresenter = presenter;
        closeButton.onClick.AddListener(onCloseButtonClicked);
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        shopPresenter.Purchased += ItemPurchased;
        buyButton.enabled = true;
        //UpdateMoneyDisplay();
    }

    public void DisplayItems(Item[] items) //아이템들 슬롯에 표시
    {
        currentItems = items;
        selectedIndex = -1;

        for (int i = 0; i < 3; i++)
        {
            slotUIs[i].SetData(items[i], i, this);
            slotUIs[i].Selected(false);
     
        }            
    }

    public void onSlotSelected(int index)
    {
        if(selectedIndex != -1 && selectedIndex != index)
        {
            slotUIs[selectedIndex].Selected(false);
        }

        selectedIndex = index;
        slotUIs[selectedIndex].Selected(true);
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
        Manager.UI.PopupText.StartSetup(message);
    }

    public override void Open()
    {
        base.Open();
        background.SetActive(true);
    }

    public void onCloseButtonClicked()
    {
        CloseButtonClicked?.Invoke();
        Close();
    }

    public override void Close()
    {
        base.Close();
        background.SetActive(false);
    }

    private void ItemPurchased()
    {
        buyButton.enabled = false;
    }
}
