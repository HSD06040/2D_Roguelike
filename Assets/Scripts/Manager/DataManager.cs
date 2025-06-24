using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : Singleton<DataManager>
{
    public Property<int> Gold = new();

    public PlayerStatus playerStatus;

    public Weapon[] WeaponDatas;
    private DataDownloader downloader;    

    private void Awake()
    {
        playerStatus = new PlayerStatus();

        WeaponDatas = Resources.LoadAll<Weapon>("Data/Weapon");

        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());        
    }
}
