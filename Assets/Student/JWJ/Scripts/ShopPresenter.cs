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
        // �������� 3�� ������ �̾Ƽ� ����Ʈ�� ��ȯ 
        currentItems = shopModel.ItemPriceInfo 
            .OrderBy(g => Guid.NewGuid())
            .Take(3)
            .ToList();

        shopview.DisplayItems(currentItems); // view ���� ���÷����� ������ ����Ʈ ����
    }

    //public void TryToBuy(string itemId)
    //{
    //    int itemPrice = currentItems<>;
    //
    //    if(moneyModel.currentMoney < itemPrice)
    //    {
    //        shopview.ShowMessage("��ȭ�� �����մϴ�!");
    //        return;
    //    }
    //    else
    //    {
    //        moneyModel.SpendMoney(itemPrice);
    //        //���⼭ ������ ����
    //    }
    //}


}
