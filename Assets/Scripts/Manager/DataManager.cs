using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : Singleton<DataManager>
{
    public Property<int> Gold = new();
    public GoldStat GoldStat = new();

    public PlayerStatus PlayerStatus = new();
    public PassiveEffectController PassiveCon;

    public Weapon[] WeaponDatas;
    public MusicWeapon[] MusicWeapons;

    private DataDownloader downloader;

    private bool isPress;
    public bool IsPress { get { return isPress; } set { isPress = value; OnPress?.Invoke(isPress); } }
    public event Action<bool> OnPress;

    private void Awake()
    {
        WeaponDatas = Resources.LoadAll<Weapon>("Data/Weapon");
        MusicWeapons = Resources.LoadAll<MusicWeapon>("Weapon");

        PassiveCon = new GameObject("PassiveCon").AddComponent<PassiveEffectController>();
        PassiveCon.transform.parent = transform;

        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());

        PlayerStatus.Speed.SetBaseStat(5);
        PlayerStatus.MaxHp.SetBaseStat(10);
        PlayerStatus.Damage.SetBaseStat(10);
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
