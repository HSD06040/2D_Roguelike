using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_PlayerWeapon : MonoBehaviour
{
    MusicWeapon musicWeapon;
    Camera cam;

    public Transform[] WeaponSpawnPos;

    public List<string> WeaponList;
    private int weaponIdx = 0;

    [Header("����")]
    [SerializeField] private MusicWeapon defaultWeapon;
    [SerializeField] private MusicWeapon[] weaponSlots = new MusicWeapon[4];
    private MusicWeapon currentWeapon;

    
    private void Start()
    {
        WeaponList = new List<string>(4);
        cam = Camera.main;
        defaultWeapon = GetComponentInChildren<MusicWeapon>();
        //defaultWeapon.Init(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentWeapon != null)
            {
                SetProjectile(currentWeapon);
                if(currentWeapon.WeaponData.itemName == "Violin")
                {
                    TestSingleton.JHT_TestInstance.IsPress = true;
                }
            }
            else
            {
                defaultWeapon.Attack(GetMousePos());
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (currentWeapon != null && currentWeapon.WeaponData.itemName == "Violin")
            {
                TestSingleton.JHT_TestInstance.IsPress = false;
            }
        }

        WeaponSwitch();
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
            weapon.Init(transform);

            musicWeapon = weapon; // �� ���� �Ҵ�
            weaponSlots[weaponIdx] = musicWeapon;
            weaponIdx++;
            return musicWeapon;
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
        //musicWeapon.Count++;
        //if (musicWeapon.Count > musicWeapon.WeaponData.WeaponMaxUpgrade)
        //{
        //    musicWeapon.Level++;
        //    musicWeapon.OnUpgrade?.Invoke(musicWeapon.Level);
        //    musicWeapon.Count = 0;
        //}
    }
    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeapon(3);
        }

    }

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];  //������ ���� = currentWeapon
        }
        else
        {
            Debug.Log((index) + " ������ �������");
        }
    }

    private void SetProjectile(MusicWeapon musicWeapon)
    {
        if (musicWeapon == null)
        {
            Debug.Log("player : musicWeapon null");
            return;
        }

        
        musicWeapon.Attack(GetMousePos());
        
        
    }

    private Vector2 GetMousePos()
    {
        Vector2 mPos = Input.mousePosition;
        Debug.Log($"JHT_PlayerWEapon : {mPos.x}, {mPos.y}");
        return cam.ScreenToWorldPoint(mPos);
    }


}
