using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class ShopPresenter
{
    private ShopView shopView;
    private List<Item> items = new();
    public event Action Purchased;

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
            random = UnityEngine.Random.Range(0, 4);
            items.Add(Manager.Data.MusicWeapons[random].WeaponData);
        }

        shopView.DisplayItems(items.ToArray()); // view 에게 디스플레이할 아이템 리스트 전달      
    }

    public void TryToBuy(int _idx)
    {
        if (_idx < 0 || _idx >= items.Count)
        {
            shopView.ShowMessage("아이템이 선택되지 않았습니다.");
            return;
        }

        var item = items[_idx];
        int itemPrice = item.Price;
    
        if(!Manager.Data.IsHaveGold(item.Price))
        {
            //Debug.Log("재화가 부족합니다!");
            shopView.ShowMessage("재화가 부족합니다!");
            Debug.Log("잔액:" + Manager.Data.Gold.Value);
            return;
        }
        
        Manager.Data.PlayerStatus.AddItem(item);  //아이템 추가

        Manager.Data.RemoveGold(item.Price);  //골드 뺌
        Debug.Log("구매됨 잔액:" + Manager.Data.Gold.Value);
        Purchased?.Invoke();
        shopView.Close();
    }
}
