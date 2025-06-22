using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    MusicWeapon musicWeapon;
    public Transform[] WeaponSpawnPos;

    public MusicWeapon[] musicWeapons;
    public List<MusicWeapon> WeaponAllList;
    //public List<string> WeaponList;
    //private int setWeaponNum;



    private void Start()
    {
        //WeaponList = new List<string>(4);
        WeaponAllList = new List<MusicWeapon>();

        for(int i=0; i < musicWeapons.Length; i++)
        {
            WeaponAllList.Add(musicWeapons[i]);
            MusicWeapon obj = Instantiate(musicWeapons[i], WeaponSpawnPos[i]).GetOrAddComponent<MusicWeapon>();
            obj.gameObject.SetActive(false);
        }
    }

    public void AddMusicWeapon(MusicWeapon _musicWeapon)
    {
        for(int i=0; i < WeaponAllList.Count; i++)
        {
            if (WeaponAllList[i].WeaponData.itemName == _musicWeapon.WeaponData.itemName 
                && !WeaponAllList[i].gameObject.activeSelf)
            {
                _musicWeapon.gameObject.SetActive(true);
                break;
            }
            else if(WeaponAllList[i].WeaponData.itemName == _musicWeapon.WeaponData.itemName
                && WeaponAllList[i].gameObject.activeSelf)
            {
                if (musicWeapon != null)
                    CheckOldWeapon();
                else
                    Debug.LogWarning("기존 무기를 찾을 수 없음.");
                break;
            }
        }
    }


    #region List<string>형식으로 weapon받아오기
    //musicWeapon에 할당하는거 다시 한번 더 생각해봐야함
    //public void AddMusicWeapon(MusicWeapon _musicWeapon)
    //{
    //    if (_musicWeapon == null) return;
    //    int notSet = 0;
    //
    //    if (!WeaponList.Contains(_musicWeapon.WeaponData.itemName))
    //    {
    //        WeaponList.Add(_musicWeapon.WeaponData.itemName);
    //        notSet = WeaponList.Count - 1; 
    //    
    //        Debug.Log($"AddMusicWeapon {_musicWeapon}");
    //        Debug.Log($"AddMusicWeapon {_musicWeapon.WeaponData.itemName}");
    //    
    //        MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[notSet]);
    //            //_musicWeapon.Spawn(WeaponSpawnPos[notSet]).GetOrAddComponent<MusicWeapon>();
    //    
    //        Debug.Log($"AddMusicWeapon {weapon}");
    //        Debug.Log($"AddMusicWeapon {weapon.WeaponData.itemName}");
    //    
    //        musicWeapon = weapon; // 새 무기 할당
    //        setWeaponNum = notSet; // 나중에 플레이어가 누른 버튼으로 바꿀거? 아몰랑
    //    }
    //    else
    //    {
    //        for (int i = 0; i < WeaponList.Count; i++)
    //        {
    //            if (WeaponList[i] == _musicWeapon.WeaponData.itemName)
    //            {
    //                notSet = i;
    //                break;
    //            }
    //        }
    //
    //        // 기존 무기를 찾기 : WeaponSpawnPos에 자식으로 존재하는 무기
    //        Transform slot = WeaponSpawnPos[notSet];
    //        musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // 기존 무기 할당
    //
    //        Debug.Log($"{WeaponList[notSet]}에 Count++");
    //        setWeaponNum = notSet; // 나중에 플레이어악 누른 버튼으로 바꿀거
    //
    //        if (musicWeapon != null)
    //            CheckOldWeapon(notSet);
    //        else
    //            Debug.LogWarning("기존 무기를 찾을 수 없음.");
    //    }
    //}
    #endregion

    public void CheckOldWeapon() // 파라미터 Player
    {
        musicWeapon.Count++;
        if (musicWeapon.Count > musicWeapon.WeaponData.WeaponMaxCount)
        {
            musicWeapon.Level++;
            musicWeapon.OnUpgrade?.Invoke(musicWeapon.Level);
        }
    }


}
