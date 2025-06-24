using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image[] weaponImages;

    private WeaponSlotPresenter presenter;

    private void Awake()
    {
        presenter = new WeaponSlotPresenter(this);
    }

    private void OnEnable()
    {
        presenter.AddEvent();
    }

    private void OnDisable()
    {
        presenter.RemoveEvent();
    }

    public void ChangeSlot(int _idx)
    {
        for (int i = 0; i < weaponImages.Length; i++)
        {
            if(i == _idx)
                weaponImages[i].color = Color.white;
            else
                weaponImages[i].color = Color.black;
        }
    }

    public void UpdateWeaponSlot(int _idx, MusicWeapon _weapon)
    {
        if (_weapon == null) return;
        
        weaponImages[_idx].sprite = _weapon.WeaponData.icon;
        weaponImages[_idx].color = Manager.Data.playerStatus.currentWeaponIdx == _idx ? Color.white : Color.black;
    }
}
