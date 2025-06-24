using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class ShopPresenter
{
    private ShopModel shopModel;
    private ShopView shopView;
    private MoneyModel moneyModel;

    private List<KeyValuePair<string, int>> currentItems = new List<KeyValuePair<string, int>>();

    public ShopPresenter(ShopModel shopModel, ShopView shopView, MoneyModel moneyModel)
    {
        this.shopModel = shopModel;
        this.shopView = shopView;
        this.moneyModel = moneyModel;
    }

    public void ShopSetting()
    {
        // 랜덤으로 3개 아이템 뽑아서 리스트로 변환 
        currentItems = shopModel.ItemPriceInfo 
            .OrderBy(g => Guid.NewGuid())
            .Take(3)
            .ToList();

        shopView.DisplayItems(currentItems); // view 에게 디스플레이할 아이템 리스트 전달
    }

    public void TryToBuy(string itemId)
    {
        var item = currentItems.Find(x => x.Key == itemId);
        int itemPrice = item.Value;
    
        if(moneyModel.currentMoney < itemPrice)
        {
            shopView.ShowMessage("재화가 부족합니다!");
            return;
        }
        else
        {
            moneyModel.SpendMoney(itemPrice);
            //shopview.UpdateMoneyDisplay();
            Debug.Log(itemId + "  " +itemPrice + "에 구매");
            shopView.CloseTheShop();
        }
    }

    public int GetCurrentMoney()
    {
        return moneyModel.currentMoney;
    }


}
