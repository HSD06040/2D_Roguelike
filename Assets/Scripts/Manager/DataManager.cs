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

    private const int defaultDamage = 4;
    private const int defaultHp = 10;
    private const int defaultSpeed = 4;
    private const int defaultAttackSpeed = 1;

    private void Awake()
    {
        WeaponDatas = Resources.LoadAll<Weapon>("Data/WeaponData");
        MusicWeapons = Resources.LoadAll<MusicWeapon>("Weapon");

        PassiveCon = new GameObject("PassiveCon").AddComponent<PassiveEffectController>();
        PassiveCon.transform.parent = transform;
        
        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            PlayerStatus.Evasion.AddModifier(100, "Test");

        if (Input.GetKeyDown(KeyCode.X))
            PlayerStatus.Damage.AddModifier(5, "Test");

        if (Input.GetKeyDown(KeyCode.C))
            Gold.Value += 100;

        if (Input.GetKeyDown(KeyCode.V))
            PlayerStatus.CurtHp.Value += 3;
    }

    private void Start()
    {
        ResetPlayerStat();
    }

    public void ResetPlayerStat()
    {
        Gold.Value = 0;
        PlayerStatus = new();

        PlayerStatus.MaxHp.SetBaseStat(defaultHp);
        PlayerStatus.Damage.SetBaseStat(defaultDamage);
        PlayerStatus.DamageMultiply.SetBaseStat(1);
        PlayerStatus.Speed.SetBaseStat(defaultSpeed);
        PlayerStatus.SpeedMultiply.SetBaseStat(1);
        PlayerStatus.AttackSpeed.SetBaseStat(defaultAttackSpeed);
        PlayerStatus.Evasion.SetBaseStat(0);

        GoldStat.InitGoldStat();
        Manager.UI.ResetUI();
        PlayerStatus.ResetItemUI();
        PlayerStatus.AddBindEvent();

        Manager.Pool.ResetPool();
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
