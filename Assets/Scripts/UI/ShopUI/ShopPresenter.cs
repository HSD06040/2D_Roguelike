using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopPresenter
{
    private ShopView shopView;
    private List<Item> items = new();

    public ShopPresenter(ShopView shopView)
    {
        this.shopView = shopView;
    }

    public void ShopSetting()
    {
        items.Clear();

        int random;

        for (int i = 0; i < 3; i++)
        {
            random = Random.Range(0, 4);
            items.Add(Manager.Data.MusicWeapons[random].WeaponData);
        }

        shopView.DisplayItems(items.ToArray()); // view ���� ���÷����� ������ ����Ʈ ����      
    }

    public void TryToBuy(int _idx)
    {
        var item = items[_idx];
        int itemPrice = item.Price;
    
        if(!Manager.Data.IsHaveGold(item.Price))
        {
            shopView.ShowMessage("��ȭ�� �����մϴ�!");
            return;
        }
        else
        {
            Manager.Data.RemoveGold(item.Price);
            //shopview.UpdateMoneyDisplay();            
            shopView.Close();
        }
    }
}
