using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    MusicWeapon musicWeapon;
    MusicWeapon curWeapon;
    public Transform[] weaponSpawnPos;
    public List<string> weaponList;
    private void Start()
    {
        weaponList = new List<string>(4);
    }


    public void AddMusicWeapon(MusicWeapon _musicWeapon)
    {
        if (_musicWeapon == null) return;
        int notSet = 0;


        if (!weaponList.Contains(_musicWeapon.weaponData.itmeName))
        {
            weaponList.Add(_musicWeapon.weaponData.itmeName);

            for(int i=0; i< weaponList.Count; i++)
            {
                if (weaponList[i] == null)
                {
                    notSet = i;
                    break;
                }
            }

            _musicWeapon.Spawn(weaponSpawnPos[notSet]);
            //_musicWeapon.SetWeaponNormalParticle(notSet);
        }
        else 
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                if (weaponList[i] == _musicWeapon.weaponData.itmeName)
                {
                    notSet = i;
                    break;
                }
            }
            Debug.Log($"{notSet}에 파티클 생성");

            _musicWeapon.CheckOldWeapon();
            //_musicWeapon.SetWeaponUpgradeParticle(notSet);
        }

    }
}
