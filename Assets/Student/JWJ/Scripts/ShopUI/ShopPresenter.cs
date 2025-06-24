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
        // �������� 3�� ������ �̾Ƽ� ����Ʈ�� ��ȯ 
        currentItems = shopModel.ItemPriceInfo 
            .OrderBy(g => Guid.NewGuid())
            .Take(3)
            .ToList();

        shopView.DisplayItems(currentItems); // view ���� ���÷����� ������ ����Ʈ ����
    }

    public void TryToBuy(string itemId)
    {
        var item = currentItems.Find(x => x.Key == itemId);
        int itemPrice = item.Value;
    
        if(moneyModel.currentMoney < itemPrice)
        {
            shopView.ShowMessage("��ȭ�� �����մϴ�!");
            return;
        }
        else
        {
            moneyModel.SpendMoney(itemPrice);
            //shopview.UpdateMoneyDisplay();
            Debug.Log(itemId + "  " +itemPrice + "�� ����");
            shopView.CloseTheShop();
        }
    }

    public int GetCurrentMoney()
    {
        return moneyModel.currentMoney;
    }


}
