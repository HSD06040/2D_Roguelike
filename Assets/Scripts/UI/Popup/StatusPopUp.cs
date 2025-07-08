using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class StatusPopUp : MonoBehaviour
{
    public Image[] WeaponSlots;
    public TextMeshProUGUI[] WeaponSlotNames;
    public TextMeshProUGUI[] WeaponSlotDamage;
    public TextMeshProUGUI PlayerSpeedText;
    public TextMeshProUGUI PlayerAttackSpeedText;

    private WeaponSlotPresenter presenter;
    private void Awake()
    {
        presenter = new WeaponSlotPresenter(this);
    }
    private void OnEnable()
    {
        presenter.AddStatusEvent();
        RefreshAllWeaponData();
    }

    public void RefreshAllWeaponData()
    {
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            var weapon = Manager.Data.PlayerStatus.PlayerWeapons[i];

            if (weapon != null)
            {
                UpdateWeaponData(i, weapon);
            } 
            else
            {
                WeaponSlots[i].sprite = null;
                WeaponSlots[i].color = Color.clear;
                WeaponSlotNames[i].text = "";
                WeaponSlotDamage[i].text = "";
            }
        }

        // 스탯도 같이 반영
        UpdatePlayerStatus(0);
    }

    private void OnDisable()
    {
        presenter.RemoveStatusEvent();
    }

    private void Start()
    {
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            WeaponSlots[i].color = Color.clear;
            WeaponSlotNames[i].text = "";
            WeaponSlotDamage[i].text = "";
        }

        PlayerSpeedText.text = Manager.Data.PlayerStatus.TotalSpeed.ToString();
        PlayerAttackSpeedText.text = Manager.Data.PlayerStatus.AttackSpeed.Value.ToString();
    }

    #region 무기 Status
    public void UpdateWeaponData(int _idx, MusicWeapon _weapon)
    {
        Debug.Log($"UpdateWeaponDAta : {_weapon.WeaponData.itemName}");
        WeaponSlots[_idx].sprite = _weapon.WeaponData.icon;
        WeaponSlots[_idx].color = WeaponSlots[_idx].sprite != null ? Color.white : Color.clear;
        
        WeaponSlotNames[_idx].text = _weapon.WeaponData.itemName;
        WeaponSlotDamage[_idx].text = _weapon.WeaponData.AttackDamage[Manager.Data.PlayerStatus.PlayerWeapons[_idx].Level].ToString();
    }

    public void UpgradeWeapon(int _idx, MusicWeapon _weapon)
    {
        WeaponSlotDamage[_idx].text = 
            _weapon.WeaponData.AttackDamage[Manager.Data.PlayerStatus.PlayerWeapons[_idx].Level].ToString();
    }
    #endregion

    public void UpdatePlayerStatus(int _idx)
    {
        PlayerSpeedText.text = Manager.Data.PlayerStatus.TotalSpeed.ToString();
        PlayerAttackSpeedText.text = Manager.Data.PlayerStatus.AttackSpeed.Value.ToString();
    }

}
