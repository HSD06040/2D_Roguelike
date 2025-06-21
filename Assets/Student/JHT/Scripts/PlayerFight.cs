using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    MusicWeapon musicWeapon;
    public Transform[] WeaponSpawnPos;
    public List<string> WeaponList;
    private void Start()
    {
        WeaponList = new List<string>(4);
    }

    //musicWeapon에 할당하는거 다시 한번 더 생각해봐야함
    public void AddMusicWeapon(MusicWeapon _musicWeapon)
    {
        if (_musicWeapon == null) return;
        int notSet = 0;

        if (!WeaponList.Contains(_musicWeapon.WeaponData.itemName))
        {
            WeaponList.Add(_musicWeapon.WeaponData.itemName);
            notSet = WeaponList.Count - 1;

            MusicWeapon weapon = _musicWeapon.Spawn(WeaponSpawnPos[notSet]).GetOrAddComponent<MusicWeapon>();
            weapon.Init(_musicWeapon.WeaponData);
            musicWeapon = weapon; // 새 무기 할당
        }
        else
        {
            for (int i = 0; i < WeaponList.Count; i++)
            {
                if (WeaponList[i] == _musicWeapon.WeaponData.itemName)
                {
                    notSet = i;
                    break;
                }
            }

            // 기존 무기를 찾기 (예: WeaponSpawnPos에 자식으로 존재하는 무기)
            Transform slot = WeaponSpawnPos[notSet];
            musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // 기존 무기 할당

            Debug.Log($"{WeaponList[notSet]}에 Count++");

            if (musicWeapon != null)
                CheckOldWeapon(notSet);
            else
                Debug.LogWarning("기존 무기를 찾을 수 없습니다.");
        }
    }


    public void CheckOldWeapon(int num) // 파라미터 Player
    {
        musicWeapon.Count++;
        if (musicWeapon.Count > musicWeapon.WeaponData.WeaponMaxCount)
        {
            musicWeapon.OnUpgrade?.Invoke(num); //이거 count아님 notSet가져와야함
        }
    }
}
