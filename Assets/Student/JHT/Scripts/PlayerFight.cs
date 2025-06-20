using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


        if (!weaponList.Contains(_musicWeapon.weaponData.itemName))
        {
            weaponList.Add(_musicWeapon.weaponData.itemName);

            for(int i=0; i< weaponList.Count; i++)
            {
                if (weaponList[i] != null)
                {
                    continue;
                }
                else
                {
                    notSet = i;
                }
            }

            MusicWeapon weapon = _musicWeapon.Spawn(weaponSpawnPos[notSet]).GetOrAddComponent<MusicWeapon>();
            weapon.Init(_musicWeapon.weaponData, _musicWeapon.ParticlePos, _musicWeapon.weaponData.icon);
            //_musicWeapon.SetWeaponNormalParticle(notSet);
        }
        else
        {
            if (_musicWeapon == null) return;

            for (int i = 0; i < weaponList.Count; i++)
            {
                if (weaponList[i] == _musicWeapon.weaponData.itemName)
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
