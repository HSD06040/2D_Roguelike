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
                    Debug.LogWarning("���� ���⸦ ã�� �� ����.");
                break;
            }
        }
    }


    #region List<string>�������� weapon�޾ƿ���
    //musicWeapon�� �Ҵ��ϴ°� �ٽ� �ѹ� �� �����غ�����
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
    //        musicWeapon = weapon; // �� ���� �Ҵ�
    //        setWeaponNum = notSet; // ���߿� �÷��̾ ���� ��ư���� �ٲܰ�? �Ƹ���
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
    //        // ���� ���⸦ ã�� : WeaponSpawnPos�� �ڽ����� �����ϴ� ����
    //        Transform slot = WeaponSpawnPos[notSet];
    //        musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // ���� ���� �Ҵ�
    //
    //        Debug.Log($"{WeaponList[notSet]}�� Count++");
    //        setWeaponNum = notSet; // ���߿� �÷��̾�� ���� ��ư���� �ٲܰ�
    //
    //        if (musicWeapon != null)
    //            CheckOldWeapon(notSet);
    //        else
    //            Debug.LogWarning("���� ���⸦ ã�� �� ����.");
    //    }
    //}
    #endregion

    public void CheckOldWeapon() // �Ķ���� Player
    {
        musicWeapon.Count++;
        if (musicWeapon.Count > musicWeapon.WeaponData.WeaponMaxCount)
        {
            musicWeapon.Level++;
            musicWeapon.OnUpgrade?.Invoke(musicWeapon.Level);
        }
    }


}
