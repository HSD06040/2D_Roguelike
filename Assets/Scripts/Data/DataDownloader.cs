using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.Networking;

public class DataDownloader
{
    private const string URL = "https://docs.google.com/spreadsheets/d/10QfD3I1AbOf_yOnV5AEE4zut2KNPh1pmiAEeALIUZzA/export?format=csv&range=B66:C66";
    // gid=2097254203 
    private const string MonsterURL = "https://docs.google.com/spreadsheets/d/1LZaGVufTNxyyq0qzsyR9IY6TX2c_TVH0/export?format=csv&gid=2097254203";
    // gid=17471850
    private const string WeaponURL = "https://docs.google.com/spreadsheets/d/1J6JrHTDnkcCYHN-F3i22qT15ako3L43B/export?format=csv&gid=17471850";

    public event Action OnDataSetupCompleted;

    public IEnumerator DownloadData()
    {
        yield return null;
        yield return LoadCSV(MonsterURL, SetupMonster, 4);
        yield return LoadCSV(WeaponURL, SetupWeapon, 4);
        OnDataSetupCompleted?.Invoke();
    }

    private IEnumerator LoadCSV(string url, Action<string[][]> onParsed, int startLine = 1)
    {
        using UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
            yield break;

        string raw = www.downloadHandler.text.Trim();
        string[] lines = raw.Split('\n');
        List<string[]> parsed = new();

        for (int i = startLine - 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Trim().Split(',');
            parsed.Add(row);
        }

        onParsed?.Invoke(parsed.ToArray());
    }

    private void SetupMonster(string[][] data)
    {
        foreach (string[] row in data)
        {
            int ID = int.Parse(row[0]);

            MonsterStat stat = Array.Find(Manager.Table.monsterStat, m => m.ID == ID);

            if(stat != null)
            {
                stat.ID = ID;
                stat.monsterName = row[1];
                stat.health = float.Parse(row[2]);
                stat.attackPower = int.Parse(row[3]);
                stat.moveSpeed = float.Parse(row[4]);
                stat.GetCoinAmount = int.Parse(row[5]);
                stat.monsterDescription = row[6];
            }
        }
    }

    private void SetupWeapon(string[][] data)
    {
        foreach (var row in data)
        {
            int ID = int.Parse(row[0]);
            int up = int.Parse(row[2]);

            Weapon weapon = Array.Find(Manager.Data.WeaponDatas, w => w.ID == ID);

            if(weapon != null)
            {                
                weapon.itemName = row[1];
                weapon.AttackDamage[up-1] = float.Parse(row[3]);
                weapon.AttackDelay[up-1] = float.Parse(row[4]);
            }
        }
    }
}
