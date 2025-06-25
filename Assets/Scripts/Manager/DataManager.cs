using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : Singleton<DataManager>
{
    public Property<int> Gold = new();

    public PlayerStatus playerStatus = new();

    public Weapon[] WeaponDatas;
    public MusicWeapon[] musicWeapons;

    private DataDownloader downloader;    

    private void Awake()
    {
        WeaponDatas = Resources.LoadAll<Weapon>("Data/Weapon");
        musicWeapons = Resources.LoadAll<MusicWeapon>("Weapon");

        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());

        playerStatus.Speed.SetBaseStat(5);
    }

    private void Start()
    {
        playerStatus.AddBindEvent();
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
