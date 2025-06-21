using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    MusicWeapon musicWeapon;
    public Transform[] WeaponSpawnPos;
    public List<string> WeaponList;
    private int setWeaponNum;


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
            setWeaponNum = notSet; // 나중에 플레이어악 누른 버튼으로 바꿀거
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
            setWeaponNum = notSet; // 나중에 플레이어악 누른 버튼으로 바꿀거

            if (musicWeapon != null)
                CheckOldWeapon(notSet);
            else
                Debug.LogWarning("기존 무기를 찾을 수 없음.");
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

    public void GoProjectile(MusicWeapon weapon,Vector3 targetPos)
    {
        if (weapon == null)
        {
            Debug.Log("Go Weapon null");
            return;
        }
        Debug.Log($"Go : {weapon.transform.name}");
        Projectile projectile = weapon.GetComponent<MusicWeapon>().WeaponData.Projectile;
        Projectile inst = Instantiate(projectile);
        inst.Init(inst.transform, targetPos);
    }

    public void GoAreaProjectile(MusicWeapon weapon,Vector3 targetPos)
    {
        if (weapon == null)
        {
            Debug.Log("Area Weapon null");
            return;
        }

        Debug.Log($"Area : {weapon.transform.name}");
        AreaProjectile areaProjectile = weapon.GetComponent<MusicWeapon>().WeaponData.AreaProjectile;
        AreaProjectile inst = Instantiate(areaProjectile);
        inst.Init(inst.transform, targetPos);
    }
}
