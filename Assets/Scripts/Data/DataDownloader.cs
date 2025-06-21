using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.Networking;

public class DataDownloader
{
    private const string URL = "https://docs.google.com/spreadsheets/d/10QfD3I1AbOf_yOnV5AEE4zut2KNPh1pmiAEeALIUZzA/export?format=csv&range=B66:C66";

    public event Action OnDataSetupCompleted;

    public IEnumerator DownloadData()
    {
        yield return null;
        //yield return LoadCSV(URL, SetupWeapon);
        //
        //OnDataSetupCompleted?.Invoke();
    }

    private IEnumerator LoadCSV(string url, Action<string[][]> onParsed)
    {
        using UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (!string.IsNullOrEmpty(www.error))
            yield break;

        string raw = www.downloadHandler.text.Trim();
        string[] lines = raw.Split('\n');
        string[][] parsed = new string[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
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
}
