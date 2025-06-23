using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

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
    

    #region List<string>형식으로 weapon받아오기
    public MusicWeapon AddMusicWeapon(MusicWeapon _musicWeapon)
    {
        if (_musicWeapon == null) return null;
        int notSet = 0;
    
        if (!WeaponList.Contains(_musicWeapon.WeaponData.itemName))
        {
            WeaponList.Add(_musicWeapon.WeaponData.itemName);
            notSet = WeaponList.Count - 1;

            MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[notSet]);
            //_musicWeapon.Spawn(WeaponSpawnPos[notSet]).GetOrAddComponent<MusicWeapon>();

            musicWeapon = weapon; // 새 무기 할당
            setWeaponNum = notSet; // 나중에 플레이어가 누른 버튼으로 바꿀거? 아몰랑
            return weapon;
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
            // 기존 무기를 찾기 : WeaponSpawnPos에 자식으로 존재하는 무기
            Transform slot = WeaponSpawnPos[notSet];
            musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // 기존 무기 할당
    
            Debug.Log($"{WeaponList[notSet]}에 Count++");
            setWeaponNum = notSet; // 나중에 플레이어악 누른 버튼으로 바꿀거
    
            if (musicWeapon != null)
                CheckOldWeapon();
            else
                Debug.LogWarning("기존 무기를 찾을 수 없음.");

            return musicWeapon;
        }
    }
    #endregion

    public void CheckOldWeapon() // 파라미터 Player
    {
        musicWeapon.Count++;
        if (musicWeapon.Count > musicWeapon.WeaponData.WeaponMaxCount)
        {
            musicWeapon.Level++;
            musicWeapon.OnUpgrade?.Invoke(musicWeapon.Level);
            musicWeapon.Count = 0;
        }
    }


}
