using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEffect : AccessoriesEffect
{
    [SerializeField] private bool isInvincible;
    [SerializeField] private GameObject prefab;
    
    public override void Active1(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible();
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Active2(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible();
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Active3(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible();
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Active4(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible();
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Revoke(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible();
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    private void CreateAttack(string _name, int _upgrade)
    {
        Manager.Data.PassiveCon.StartSkillCoroutine(prefab, $"{_name}_{_upgrade}", delays[_upgrade]);
    }

    private void PlayerInvincible()
    {
        Manager.Data.PassiveCon.PlayerInvincible(1);
    }
}
