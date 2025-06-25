using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotPresenter
{
    private WeaponSlot weaponSlot;

    public WeaponSlotPresenter(WeaponSlot _weaponSlot)
    {
        weaponSlot = _weaponSlot;
    }

    public void AddEvent()
    {
        Manager.Data.PlayerStatus.OnChangedWeapon += weaponSlot.UpdateWeaponSlot;
        Manager.Data.PlayerStatus.OnCurrentWeaponChanged += weaponSlot.ChangeSlot;
    }

    public void RemoveEvent()
    {
        Manager.Data.PlayerStatus.OnChangedWeapon -= weaponSlot.UpdateWeaponSlot;
        Manager.Data.PlayerStatus.OnCurrentWeaponChanged -= weaponSlot.ChangeSlot;
    }
}
