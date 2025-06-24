using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    // �÷��̾��    
    public IntStat Hp { get; private set; } = new(); // ü�� ��
    public IntStat Damage { get; private set; } = new(); // ������ ��
    public FloatStat Speed { get; private set; } = new(); // ���ǵ� ��
    public FloatStat AttackSpeed { get; private set; } = new(); // �ۼ�Ʈ

    // �÷��̾ ���� ������ �ִ� ���� (���߿� �߰�)
    private const int weaponCount = 4;

    private MusicWeapon weapon;
    public int currentWeaponIdx = 0;

    public MusicWeapon[] PlayerWeapons = new MusicWeapon[4];
    private List<string> WeaponList = new List<string>(4);

    public Action<int, MusicWeapon> OnChangedWeapon;
    public Action<int> OnCurrentWeaponChanged;

    public void AddWeapon(MusicWeaponType musicWeaponType)
    {
        MusicWeapon _weapon = Manager.Data.musicWeapons[(int)musicWeaponType];

        if (_weapon == null) return;

        int idx = 0;

        if(!WeaponList.Contains(_weapon.WeaponData.itemName))
        {
            WeaponList.Add(_weapon.WeaponData.itemName);

            idx = EmptySlots();
            if (idx == -1) return;
  
            OnChangedWeapon.Invoke(idx, _weapon);
        }
        else
        {          
            for (int i = 0; i < PlayerWeapons.Length; i ++)
            {
                if (PlayerWeapons[i].WeaponData.itemName == _weapon.WeaponData.itemName)
                {
                    idx = i;
                    weapon = PlayerWeapons[i];
                }
            }

            weapon.Count++;
            if (weapon.Count >= weapon.WeaponData.WeaponMaxCount)
            {
                weapon.Level++;
                weapon.OnUpgrade?.Invoke(weapon.Level);
                weapon.Count = 0;
            }
        }
    }
    
    public void AddBindEvent()
    {
        Manager.Input.GetPlayerBind("Weapon1").AddStartedEvent(() => WeaponChanged(0));
        Manager.Input.GetPlayerBind("Weapon2").AddStartedEvent(() => WeaponChanged(1));
        Manager.Input.GetPlayerBind("Weapon3").AddStartedEvent(() => WeaponChanged(2));
        Manager.Input.GetPlayerBind("Weapon4").AddStartedEvent(() => WeaponChanged(3));
    }

    public void DisableWeaponBind()
    {
        Manager.Input.GetPlayerBind("Weapon1").ActionDisable();
        Manager.Input.GetPlayerBind("Weapon2").ActionDisable();
        Manager.Input.GetPlayerBind("Weapon3").ActionDisable();
        Manager.Input.GetPlayerBind("Weapon4").ActionDisable();
    }

    private void WeaponChanged(int _idx)
    {
        if (PlayerWeapons[_idx] == null) return;

        currentWeaponIdx = _idx;
        OnCurrentWeaponChanged?.Invoke(_idx);
    }

    private int EmptySlots()
    {
        for (int i = 0; i < PlayerWeapons.Length; i++)
        {
            if (PlayerWeapons[i] == null)
                return i;
        }

        return -1;
    }    
}
