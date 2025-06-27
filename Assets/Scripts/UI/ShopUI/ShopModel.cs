
using System.Collections.Generic;
using UnityEngine;


public class ShopModel
{
    private Dictionary<string, int> itemPriceInfo;

    public ShopModel()
    {
        itemPriceInfo = new Dictionary<string, int>  //상점 아이템 목록, 가격
        {
            {"item1", 30},
            {"item2", 50},
            {"item3", 70},
            {"item4", 100},
            {"item5", 120},
            {"item6", 140}
        };
    }
 
    public Dictionary<string, int> ItemPriceInfo => itemPriceInfo; //읽기 전용
}
