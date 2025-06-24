using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class ShopPresenter
{
    private ShopModel shopModel;
    private ShopView shopview;
    private MoneyModel moneyModel;

    private List<KeyValuePair<string, int>> currentItems = new List<KeyValuePair<string, int>>();

    public ShopPresenter(ShopModel shopModel, ShopView shopview, MoneyModel moneyModel)
    {
        this.shopModel = shopModel;
        this.shopview = shopview;
        this.moneyModel = moneyModel;
    }

    public void ShopSetting()
    {
        // 랜덤으로 3개 아이템 뽑아서 리스트로 변환 
        currentItems = shopModel.ItemPriceInfo 
            .OrderBy(g => Guid.NewGuid())
            .Take(3)
            .ToList();

        shopview.DisplayItems(currentItems); // view 에게 디스플레이할 아이템 리스트 전달
    }

    //public void TryToBuy(string itemId)
    //{
    //    int itemPrice = currentItems<>;
    //
    //    if(moneyModel.currentMoney < itemPrice)
    //    {
    //        shopview.ShowMessage("재화가 부족합니다!");
    //        return;
    //    }
    //    else
    //    {
    //        moneyModel.SpendMoney(itemPrice);
    //        //여기서 아이템 지급
    //    }
    //}


}
