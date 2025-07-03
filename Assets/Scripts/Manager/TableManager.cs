using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    public GameObject[] chapter1Monster;
    public GameObject[] chapter2Monster;
    public GameObject[] chapter3Monster;
    public MonsterStat[] monsterStat;
    
    public Accessories[] acces;
    public UseItem[] useItems;

    private void Awake()
    {
        chapter1Monster = Resources.LoadAll<GameObject>("Monster/Chapter1");
        chapter2Monster = Resources.LoadAll<GameObject>("Monster/Chapter2");
        chapter3Monster = Resources.LoadAll<GameObject>("Monster/Chapter3");

        acces = Resources.LoadAll<Accessories>("Data/Item/Accessories");
        useItems = Resources.LoadAll<UseItem>("Data/Item/UseItem");
    }

    public Item GetRandomItem()
    {
        List<Item> allItems = GetAllItems();
        int random = Random.Range(0, allItems.Count);
        return allItems[random];
    }

    public List<Item> GetAllItems()
    {
        List<Item> allItems = new();

        allItems.AddRange(acces);

        allItems.AddRange(useItems);

        return allItems;
    }
}
