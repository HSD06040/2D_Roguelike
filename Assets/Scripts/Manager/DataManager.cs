using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : Singleton<DataManager>
{
    public Weapon[] WeaponDatas;
    private DataDownloader downloader;

    private void Awake()
    {
        WeaponDatas = Resources.LoadAll<Weapon>("Data/Weapon");

        downloader = new DataDownloader();
        StartCoroutine(downloader.DownloadData());     
    }
}
