using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusPopUp : MonoBehaviour
{
    public Image[] weaponSlots;
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
    }

    private void OnDisable()
    {
        presenter.RemoveStatusEvent();
    }

    void Start()
    {
        weaponSlots = new Image[4];
        WeaponSlotNames = new TextMeshProUGUI[4];
        WeaponSlotDamage = new TextMeshProUGUI[4];
    }

    public void UpdateWeaponData(int _idx, MusicWeapon _weapon)
    {
        Debug.Log($"{_idx}¿¡ {_weapon.WeaponData.itemName}Ãß°¡");
        if (_weapon == null)
        {
            weaponSlots[_idx].sprite = null;
            weaponSlots[_idx].color = Manager.Data.PlayerStatus.currentWeaponIdx == _idx ? Color.white : Color.clear;
            return;
        }
        weaponSlots[_idx].sprite = _weapon.WeaponData.icon;
        weaponSlots[_idx].color = Manager.Data.PlayerStatus.currentWeaponIdx == _idx ? Color.white : Color.clear;
        WeaponSlotNames[_idx].text = _weapon.WeaponData.itemName;
        WeaponSlotDamage[_idx].text = _weapon.WeaponData.AttackDamage.ToString();
    }

    public void UpdatePlayerStatus()
    {
        PlayerSpeedText.text = Manager.Data.PlayerStatus.Speed.ToString();
        PlayerAttackSpeedText.text = Manager.Data.PlayerStatus.AttackSpeed.ToString();
    }

}
