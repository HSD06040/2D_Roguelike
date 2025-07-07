using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotPresenter
{
    private WeaponSlot weaponSlot;
    private StatusPopUp statusPopUp;

    public WeaponSlotPresenter(WeaponSlot _weaponSlot)
    {
        weaponSlot = _weaponSlot;
    }

    public WeaponSlotPresenter(StatusPopUp _statusPopUp)
    {
        statusPopUp = _statusPopUp;
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

    #region StatusPopUp

    public void AddStatusEvent()
    {
        Manager.Data.PlayerStatus.OnAddWeapon += statusPopUp.UpdateWeaponData;
        Manager.Data.PlayerStatus.OnUpgradedWeapon += statusPopUp.UpgradeWeapon;
    }

    public void RemoveStatusEvent()
    {
        Manager.Data.PlayerStatus.OnAddWeapon -= statusPopUp.UpdateWeaponData;
        Manager.Data.PlayerStatus.OnUpgradedWeapon -= statusPopUp.UpgradeWeapon;
    }

    #endregion
}
