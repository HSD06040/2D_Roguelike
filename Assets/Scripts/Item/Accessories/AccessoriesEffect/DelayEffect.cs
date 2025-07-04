using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Delay Effect", menuName = "Item/Accessories/Effect/Delay Effect")]
public class DelayEffect : AccessoriesEffect
{
    [Header("Delay Effect Invincible")]
    [SerializeField] private bool isInvincible;
    [SerializeField, Tooltip("몇초 무적")] private float invincibleTime;

    [Header("Delay Effect Data")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int[] counts;
    [SerializeField] private float[] damages;
    [SerializeField] private float[] radius;
    [SerializeField] private float[] delays;
    
    public override void Active1(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible(accessories.itemName, accessories.UpgradeIdx);
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Active2(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible(accessories.itemName, accessories.UpgradeIdx);
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Active3(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible(accessories.itemName, accessories.UpgradeIdx);
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Active4(Accessories accessories)
    {
        if (isInvincible)
            PlayerInvincible(accessories.itemName, accessories.UpgradeIdx);
        else
            CreateAttack(accessories.itemName, accessories.UpgradeIdx);
    }

    public override void Revoke(Accessories accessories)
    {
        if (isInvincible)
            Manager.Data.PassiveCon.StopCoroutine($"{accessories.itemName}_{accessories.UpgradeIdx}");
        else
            Manager.Data.PassiveCon.StopSkillCoroutine($"{accessories.itemName}_{accessories.UpgradeIdx}");
    }

    private void CreateAttack(string _name, int _upgrade)
    {
        Manager.Data.PassiveCon.StartSkillCoroutine
            (prefab, $"{_name}_{_upgrade}", intervals[_upgrade], counts[_upgrade], delays[_upgrade], damages[_upgrade], radius[_upgrade]);
    }

    private void PlayerInvincible(string _itemName, int _upgrade)
    {
        Manager.Data.PassiveCon.PlayerInvincible(invincibleTime, intervals[_upgrade], $"{_itemName}_{_upgrade}", prefab);
    }
}
