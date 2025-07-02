using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.Networking;

public class DataDownloader
{
    private const string URL = "https://docs.google.com/spreadsheets/d/10QfD3I1AbOf_yOnV5AEE4zut2KNPh1pmiAEeALIUZzA/export?format=csv&range=B66:C66";
    // gid=1209965616
    private const string MonsterURL = "https://docs.google.com/spreadsheets/d/1zctuhRCS5Zo979q3ffnjXye-MBmceOYc/export?format=csv&gid=1209965616";

    public event Action OnDataSetupCompleted;

    public IEnumerator DownloadData()
    {
        yield return null;
        //yield return LoadCSV(URL, SetupWeapon);
        //
        //OnDataSetupCompleted?.Invoke();
    }

    private IEnumerator LoadCSV(string url, Action<string[][]> onParsed, int startLine = 1)
    {
        using UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
            yield break;

        string raw = www.downloadHandler.text.Trim();
        string[] lines = raw.Split('\n');
        string[][] parsed = new string[lines.Length][];

        for (int i = startLine - 1; i < lines.Length; i++)
        {
            parsed[i] = lines[i].Trim().Split(',');
        }

        onParsed?.Invoke(parsed);
    }

    private void SetupWeapon(string[][] data)
    {
        foreach (var row in data)
        {
            string weaponName = row[0];
            int damage = int.Parse(row[1]);
            
            Weapon weapon = Array.Find(Manager.Data.WeaponDatas, w => w.name == weaponName);

            if (weapon != null)
            {
                weapon.AttackDamage = damage;                                
            }
        }
    }

    private void SetupMonster(string[][] data)
    {
        foreach (var row in data)
        {
            int monsterID = int.Parse(row[0]);

            MonsterStat stat = Array.Find(Manager.Table.monsterStat, m => m.ID == monsterID);

            if(stat != null)
            {
                stat.monsterName = row[1];
                stat.health = float.Parse(row[2]);
                stat.attackPower = int.Parse(row[3]);
                stat.moveSpeed = float.Parse(row[4]);
                stat.monsterDescription = row[5];
            }
        }
    }
}
