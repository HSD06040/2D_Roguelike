using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleEffect : AccessoriesEffect
{
    [SerializeField] private float[] invincibleTimes;

    public override void Active1(Accessories accessories)
    {
        Playerinvincible(accessories.UpgradeIdx);
    }

    public override void Active2(Accessories accessories)
    {
        Playerinvincible(accessories.UpgradeIdx);
    }

    public override void Active3(Accessories accessories)
    {
        Playerinvincible(accessories.UpgradeIdx);
    }

    public override void Active4(Accessories accessories)
    {
        Playerinvincible(accessories.UpgradeIdx);
    }

    public override void Revoke(Accessories accessories)
    {
        if (!isActive) return;

        Manager.Data.PassiveCon.StopPlayerInvincible();
        isActive = false;
    }

    private void Playerinvincible(int _upgrade)
    {
        if (isActive) return;

        Manager.Data.PassiveCon.PlayerInvincible(invincibleTimes[_upgrade]);
        isActive = true;
    }
}
