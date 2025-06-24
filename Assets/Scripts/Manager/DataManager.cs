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
    }

    private void Start()
    {
        playerStatus.AddBindEvent();
    }
}
