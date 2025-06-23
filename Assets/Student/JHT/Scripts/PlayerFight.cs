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
    

    #region List<string>�������� weapon�޾ƿ���
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

            musicWeapon = weapon; // �� ���� �Ҵ�
            setWeaponNum = notSet; // ���߿� �÷��̾ ���� ��ư���� �ٲܰ�? �Ƹ���
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
            // ���� ���⸦ ã�� : WeaponSpawnPos�� �ڽ����� �����ϴ� ����
            Transform slot = WeaponSpawnPos[notSet];
            musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // ���� ���� �Ҵ�
    
            Debug.Log($"{WeaponList[notSet]}�� Count++");
            setWeaponNum = notSet; // ���߿� �÷��̾�� ���� ��ư���� �ٲܰ�
    
            if (musicWeapon != null)
                CheckOldWeapon();
            else
                Debug.LogWarning("���� ���⸦ ã�� �� ����.");

            return musicWeapon;
        }
    }
    #endregion

    public void CheckOldWeapon() // �Ķ���� Player
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
