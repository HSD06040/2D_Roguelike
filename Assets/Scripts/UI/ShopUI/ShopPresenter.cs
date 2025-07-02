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

        shopView.DisplayItems(items.ToArray()); // view ���� ���÷����� ������ ����Ʈ ����      
    }

    public void TryToBuy(int _idx)
    {
        if (_idx < 0 || _idx >= items.Count)
        {
            shopView.ShowMessage("�������� ���õ��� �ʾҽ��ϴ�.");
            return;
        }

        var item = items[_idx];
        int itemPrice = item.Price;
    
        if(!Manager.Data.IsHaveGold(item.Price))
        {
            //Debug.Log("��ȭ�� �����մϴ�!");
            shopView.ShowMessage("��ȭ�� �����մϴ�!");
            Debug.Log("�ܾ�:" + Manager.Data.Gold.Value);
            return;
        }
        
        Manager.Data.PlayerStatus.AddItem(item);  //������ �߰�

        Manager.Data.RemoveGold(item.Price);  //��� ��
        Debug.Log("���ŵ� �ܾ�:" + Manager.Data.Gold.Value);
        Purchased?.Invoke();
        shopView.Close();
    }
}
