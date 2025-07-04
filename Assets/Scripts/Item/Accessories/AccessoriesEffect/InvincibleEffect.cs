using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InvincibleEffect", menuName = "Item/Accessories/Effect/InvincibleEffect")]
public class InvincibleEffect : AccessoriesEffect
{
    [Header("Invincible Effect")]
    [SerializeField] private float[] invincibleTimes;
    [SerializeField] private GameObject prefab;

    public override void Active1(Accessories accessories) => Playerinvincible(accessories.itemName, accessories.UpgradeIdx);

    public override void Active2(Accessories accessories) => Playerinvincible(accessories.itemName, accessories.UpgradeIdx);

    public override void Active3(Accessories accessories) => Playerinvincible(accessories.itemName, accessories.UpgradeIdx);

    public override void Active4(Accessories accessories) => Playerinvincible(accessories.itemName, accessories.UpgradeIdx);

    public override void Revoke(Accessories accessories)
    {
        if (!isActive) return;

        Manager.Data.PassiveCon.StopPlayerInvincible($"{accessories.itemName}_{accessories.UpgradeIdx}");
        isActive = false;
    }

    private void Playerinvincible(string _itemName, int _upgrade)
    {
        if (isActive) return;

        Manager.Data.PassiveCon.PlayerInvincible(invincibleTimes[_upgrade], intervals[_upgrade], $"{_itemName}_{_upgrade}", prefab);
    }
}
