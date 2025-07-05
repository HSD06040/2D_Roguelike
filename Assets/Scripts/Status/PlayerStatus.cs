using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

public enum StatType
{
    MaxHp, Damage, Speed, AttackSpeed, CurrentHP, Evasion, DamageMultiply, SpeedMultiply
}

[Serializable]
public class PlayerStatus
{
    // 플레이어스텟   
    [field : SerializeField] public IntStat MaxHp { get; private set; } = new(); // 최대 체력 값

    [field: SerializeField] public FloatStat Damage { get; private set; } = new(); // 데미지 값
    [field: SerializeField] public FloatStat DamageMultiply { get; private set; } = new(); // 데미지 퍼센트

    [field: SerializeField] public FloatStat Speed { get; private set; } = new(); // 스피드 값
    [field: SerializeField] public FloatStat SpeedMultiply { get; private set; } = new(); // 스피드 값

    [field: SerializeField] public FloatStat AttackSpeed { get; private set; } = new(); // 발사체 연사속도 퍼센트    
    [field: SerializeField] public FloatStat Evasion { get; private set; } = new(); // 회피율 퍼센트
     
    public Property<int> CurtHp = new Property<int>();

    public bool Invincible;
    
    private const int weaponCount = 4;
    private const int accessoriesCount = 2;

    [Header("Accessories")]
    public Accessories[] PlayerAccessories = new Accessories[accessoriesCount];

    public Action<int, Accessories> OnAccessoriesChanged;

    [Header("Weapon")]
    //private MusicWeapon weapon;
    public int currentWeaponIdx;
    public MusicWeapon curWeapon => PlayerWeapons[currentWeaponIdx];
    public MusicWeapon[] PlayerWeapons = new MusicWeapon[weaponCount];
    private List<MusicWeaponType> WeaponList = new List<MusicWeaponType>(4);

    public Action<int, MusicWeapon> OnChangedWeapon;
    public event Action<int> OnCurrentWeaponChanged;

    public Action OnPlayerDead;

    public float TotalDamage => (weaponDamage + Damage.Value) * DamageMultiply.Value;
    public float TotalSpeed => Speed.Value * SpeedMultiply.Value;
    private float weaponDamage => curWeapon ? curWeapon.WeaponData.AttackDamage[curWeapon.Level] : 1;

    #region Bind
    public void AddBindEvent()
    {        
        Manager.Input.GetPlayerBind("WeaponSelect").AddStartedEvent(OnWeaponSelect);  
    }

    public void DisableWeaponBind()
    {
        Manager.Input.GetPlayerBind("WeaponSelect").RemoveStartedEvent(OnWeaponSelect);
    }
    #endregion

    public void AddItem(Item item)
    {
        if(item is Weapon weapon)
        {
            AddWeapon(weapon.Type);
        }
        else if (item is UseItem useItem)
        {
            useItem.Execute();
        }
        else if (item is Accessories acc)
        {
            TryEquipAccessories(acc);
        }
    }

    #region Weapon
    public void AddWeapon(MusicWeaponType musicWeaponType)
    {
        MusicWeapon _weapon = Manager.Data.MusicWeapons[(int)musicWeaponType];

        if (_weapon == null) return;
        int idx = 0;

        if (!WeaponList.Contains(_weapon.WeaponData.Type))
        {
            WeaponList.Add(_weapon.WeaponData.Type);
            _weapon.curAttackDamage = _weapon.WeaponData.AttackDamage[0]; // 아닐수도있음
            _weapon.curAttackDelay = _weapon.WeaponData.AttackDelay[0];
            idx = EmptyWeaponSlot();
            PlayerWeapons[idx] = _weapon; //이부분이 없어서 slot에 안 들어갔음
            Debug.Log(idx);

            if (idx == -1) return;

            OnChangedWeapon?.Invoke(idx, _weapon);
        }
        else
        {
            for (int i = 0; i < PlayerWeapons.Length; i++)
            {
                if (PlayerWeapons[i].WeaponData.Type == _weapon.WeaponData.Type)
                {
                    idx = i;
                    //weapon = PlayerWeapons[i];
                    break;
                }
                else
                {
                    continue;
                }
            }
            Debug.Log($"Add : {idx}");
            if (PlayerWeapons[idx].Level <= PlayerWeapons[idx].WeaponData.WeaponMaxUpgrade)
            {
                Debug.Log($"{PlayerWeapons[idx].WeaponData.itemName} LevelUp");
                PlayerWeapons[idx].Level++;
                PlayerWeapons[idx].OnUpgrade?.Invoke(PlayerWeapons[idx].Level);
            }
        }
    }

    private void OnWeaponSelect(InputAction.CallbackContext ctx)
    {
        int idx = ctx.control.name switch
        {
            "1" => 0,
            "2" => 1,
            "3" => 2,
            "4" => 3,
            _ => -1
        };

        if (idx >= 0)
            WeaponChanged(idx);
    }

    private void WeaponChanged(int _idx)
    {
        if (PlayerWeapons[_idx] == null) return;
        Debug.Log($"WeaponChanged : {_idx}");
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
    public void TryEquipAccessories(Accessories accessories)
    {
        Accessories match = null;

        foreach (var item in PlayerAccessories)
        {
            if (item == null) continue;

            if(item.ID == accessories.ID)
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
            Manager.UI.OpenAccessoriesChangepanel(accessories);
        }
        else
        {
            EquipSlotAccessories(accessories, slotIdx);
        }
    }

    public void EquipSlotAccessories(Accessories accessories, int _idx)
    {
        PlayerAccessories[_idx] = accessories;

        if (PlayerAccessories[_idx].Effect != null)
            PlayerAccessories[_idx].Effect.RegisterEvents(PlayerAccessories[_idx]);
            
        PlayerAccessories[_idx].AddStat();

        OnAccessoriesChanged?.Invoke(_idx, accessories);
    }

    public void UnEquipAccessories(int _idx)
    {
        if (PlayerAccessories[_idx] == null) return;

        if (PlayerAccessories[_idx].Effect != null)
        {
            PlayerAccessories[_idx].Effect.Revoke(PlayerAccessories[_idx]);
            PlayerAccessories[_idx].Effect.UnregisterEvents();
        }

        PlayerAccessories[_idx].RemoveStat();

        PlayerAccessories[_idx] = null;
    }

    public void ChangeAccessories(Accessories _ac ,int _idx)
    {
        if (_ac == null) return;

        UnEquipAccessories(_idx);
        EquipSlotAccessories(_ac, _idx);
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
        accessories.Upgrade();
    }
    #endregion

    #region Stat
    public float GetStat(StatType type)
    {
        return (type) switch
        {
            StatType.MaxHp => MaxHp.Value,
            StatType.Damage => TotalDamage,
            StatType.Speed => TotalSpeed,
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
            case StatType.Evasion: Evasion.AddModifier(amount, source); break;
            case StatType.DamageMultiply: DamageMultiply.AddModifier(amount, source); break;
            case StatType.SpeedMultiply: SpeedMultiply.AddModifier(amount, source); break;
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
        if (Evasion.Value != 0)
        {
            if (UnityEngine.Random.Range(0, 100) < Evasion.Value)
                return true;
        }

        CurtHp.Value -= amount;

        if(CurtHp.Value <= 0)
        {
            OnPlayerDead?.Invoke();
            return false;
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

    public void ResetItemUI()
    {
        for (int i = 0; i < weaponCount; i++)
        {
            OnChangedWeapon?.Invoke(i, null);
        }

        for (int i = 0; i < accessoriesCount; i++)
        {
            OnAccessoriesChanged?.Invoke(i, null);
        }
    }
}
