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


    private void Awake()
    {
        WeaponDatas = Resources.LoadAll<Weapon>("Data/WeaponData");
        MusicWeapons = Resources.LoadAll<MusicWeapon>("Weapon");

        PassiveCon = new GameObject("PassiveCon").AddComponent<PassiveEffectController>();
        PassiveCon.transform.parent = transform;

        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());        
    }

    private void Start()
    {
        ResetPlayerStat();
    }

    public void ResetPlayerStat()
    {
        Gold.Value = 0;
        PlayerStatus = new();

        PlayerStatus.MaxHp.SetBaseStat(10);
        PlayerStatus.Speed.SetBaseStat(5);
        PlayerStatus.SpeedMultiply.SetBaseStat(1);
        PlayerStatus.DamageMultiply.SetBaseStat(1);
        PlayerStatus.Damage.SetBaseStat(10);
        PlayerStatus.AttackSpeed.SetBaseStat(1);
        PlayerStatus.Evasion.SetBaseStat(0);

        GoldStat.InitGoldStat();
        PlayerStatus.AddBindEvent();
        Manager.UI.ResetUI();
        PlayerStatus.ResetItems();

        Manager.Pool.ResetPool();
        PassiveCon.Find();
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
