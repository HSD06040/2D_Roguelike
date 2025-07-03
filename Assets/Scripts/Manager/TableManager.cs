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
        chapter1Monster = Resources.LoadAll<GameObject>("Monster/Chapter1_Monster");
        chapter2Monster = Resources.LoadAll<GameObject>("Monster/Chapter2_Monster");
        chapter3Monster = Resources.LoadAll<GameObject>("Monster/Chapter3_Monster");
        monsterStat = Resources.LoadAll<MonsterStat>("Data/Monster");

        acces = Resources.LoadAll<Accessories>("Data/Item/Accessories");
        useItems = Resources.LoadAll<UseItem>("Data/Item/UseItem");
    }

    public Item GetRandomItem()
    {
        List<Item> allItems = GetAllItems();
        int random = Random.Range(0, allItems.Count);
        return allItems[random];
    }

    public GameObject RandomMonsterSpawn(int stage)
    {
        GameObject obj = null;
        switch (stage)
        {
            case 1: return chapter1Monster[Random.Range(0, chapter1Monster.Length)];
            case 2: return chapter2Monster[Random.Range(0, chapter2Monster.Length)];
            case 3: return chapter3Monster[Random.Range(0, chapter3Monster.Length)];
        }

        return obj;
    }

    public List<Item> GetAllItems()
    {
        List<Item> allItems = new();

        allItems.AddRange(acces);

        allItems.AddRange(useItems);

        return allItems;
    }
}
