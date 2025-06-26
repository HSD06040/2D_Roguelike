using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public enum StatType
{
    MaxHp,Damage,Speed,AttackSpeed,CurrentHP
}

[Serializable]
public class PlayerStatus
{
    // 플레이어스텟        
    public IntStat MaxHp { get; private set; } = new(); // 최대 체력 값
    public FloatStat Damage { get; private set; } = new(); // 데미지 값
    public FloatStat Speed { get; private set; } = new(); // 스피드 값
    public FloatStat AttackSpeed { get; private set; } = new(); // 퍼센트

    public Property<int> CurtHp = new Property<int>();

    public bool Invincible;
    // 플레이어가 현재 가지고 있는 무기 (나중에 추가)
    private const int weaponCount = 4;
    private const int accessoriesCount = 2;

    [Header("Accessories")]
    public Accessories[] PlayerAccessories = new Accessories[accessoriesCount];

    [Header("Weapon")]
    private MusicWeapon weapon;
    public int currentWeaponIdx;
    public MusicWeapon[] PlayerWeapons = new MusicWeapon[weaponCount];
    private List<string> WeaponList = new List<string>(4);

    public Action<int, MusicWeapon> OnChangedWeapon;
    public Action<int> OnCurrentWeaponChanged;

    public Action OnPlayerDead;

    #region Bind
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
    #endregion

    #region Weapon
    public void AddWeapon(MusicWeaponType musicWeaponType)
    {
        MusicWeapon _weapon = Manager.Data.MusicWeapons[(int)musicWeaponType];

        if (_weapon == null) return;

        int idx = 0;

        if (!WeaponList.Contains(_weapon.WeaponData.itemName))
        {
            WeaponList.Add(_weapon.WeaponData.itemName);

            idx = EmptyWeaponSlot();
            if (idx == -1) return;

            OnChangedWeapon.Invoke(idx, _weapon);
        }
        else
        {
            for (int i = 0; i < PlayerWeapons.Length; i++)
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

    private void WeaponChanged(int _idx)
    {
        if (PlayerWeapons[_idx] == null) return;

        currentWeaponIdx = _idx;
        OnCurrentWeaponChanged?.Invoke(_idx);
    }

    private int EmptyWeaponSlot()
    {
        for (int i = 0; i < PlayerWeapons.Length; i++)
        {
            if (PlayerWeapons[i] == null)
                return i;
        }

        return -1;
    }
    #endregion

    #region Accessories
    public void EquipAccessories(Accessories accessories)
    {
        Accessories match = null;

        foreach (var item in PlayerAccessories)
        {
            if(item.itemName == accessories.itemName)
            {
                match = item;
                break;
            }
        }

        if (match != null)
        {
            UpgradeAccessories(match);
            return;
        }

        int slotIdx = EmptyAccessoriesSlot();

        if(slotIdx == -1)
        {
            // Change UI 오픈
            Debug.Log("자리가 없어 바꾸는 UI를 Open합니다.");
        }
        else
        {
            PlayerAccessories[slotIdx] = accessories;
        }
    }

    public void UnEquipAccessories(int _idx)
    {
        if (PlayerAccessories[_idx] == null) return;

        //PlayerAccessories[_idx].Effect.Revoke(PlayerAccessories[_idx].itemName, PlayerAccessories[_idx].UpgradeIdx);
        PlayerAccessories[_idx] = null;
    }

    private int EmptyAccessoriesSlot()
    {
        for(int i = 0;i < PlayerAccessories.Length;i++)
        {
            if (PlayerAccessories[i] == null)
                return i;
        }
        return -1;
    }

    private void UpgradeAccessories(Accessories accessories)
    {

    }
    #endregion

    #region Stat
    public float GetStat(StatType type)
    {
        return (type) switch
        {
            StatType.MaxHp => MaxHp.Value,
            StatType.Damage => Damage.Value,
            StatType.Speed => Speed.Value,
            StatType.AttackSpeed => AttackSpeed.Value,
            StatType.CurrentHP => CurtHp.Value,
            _ => 0
        };
    }

    public void AddStat(StatType type, float amount, string source)
    {
        switch (type)
        {
            case StatType.MaxHp: MaxHp.AddModifier((int)amount, source); break;
            case StatType.Damage: Damage.AddModifier(amount, source); break;
            case StatType.AttackSpeed: AttackSpeed.AddModifier(amount, source); break;
            case StatType.Speed: Speed.AddModifier(amount, source); break;
        }
    }

    public void RemoveStat(StatType type, string source)
    {
        switch (type)
        {
            case StatType.MaxHp: MaxHp.RemoveModifier(source); break;
            case StatType.Damage: Damage.RemoveModifier(source); break;
            case StatType.AttackSpeed: AttackSpeed.RemoveModifier(source); break;
            case StatType.Speed: Speed.RemoveModifier(source); break;
        }
    }
    #endregion

    public bool DecreaseHealth(int amount)
    {
        CurtHp.Value -= amount;

        if(CurtHp.Value <= 0)
        {
            OnPlayerDead?.Invoke();
            return true;
        }

        return false;
    }

    public void IncreaceHealth(int amount)
    {
        if(CurtHp.Value + amount > MaxHp.Value)
        {
            CurtHp.Value = MaxHp.Value;
        }        
        else
            CurtHp.Value += amount;
    }
}
