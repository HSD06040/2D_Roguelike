using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : Singleton<DataManager>
{
    public Property<int> Gold = new();

    public PlayerStatus PlayerStatus = new();

    public Weapon[] WeaponDatas;
    public MusicWeapon[] musicWeapons;

    private DataDownloader downloader;    

    private void Awake()
    {
        WeaponDatas = Resources.LoadAll<Weapon>("Data/Weapon");
        musicWeapons = Resources.LoadAll<MusicWeapon>("Weapon");

        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());

        PlayerStatus.Speed.SetBaseStat(5);

        PlayerStatus.MaxHp.SetBaseStat(10);
    }

    private void Start()
    {
        PlayerStatus.AddBindEvent();
    }

    public bool IsHaveGold(int amount)
    {
        return Gold.Value >= amount;
    }

    public void RemoveGold(int amount)
    {
        if(IsHaveGold(amount))
        {
            Gold.Value -= amount;
        }
    }

    public void AddGold(int amount) => Gold.Value += amount;    
}
