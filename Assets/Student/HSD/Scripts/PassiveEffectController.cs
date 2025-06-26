using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEffectController : MonoBehaviour
{
    private Coroutine invincibleRoutine;
    private Accessories[] accessories;

    private void Start()
    {
        accessories = Manager.Data.PlayerStatus.PlayerAccessories;
    }

    public void PlayerInvincible(float delay)
    {
        if (invincibleRoutine != null)
        {
            StopCoroutine(invincibleRoutine);
            invincibleRoutine = null;
        }
        invincibleRoutine = StartCoroutine(PlayerInvincibleRoutine(delay));
    }

    public void StopPlayerInvincible()
    {
        if (invincibleRoutine != null)
        {
            StopCoroutine(invincibleRoutine);
            invincibleRoutine = null;
        }
    }

    private IEnumerator PlayerInvincibleRoutine(float delay)
    {
        Manager.Data.PlayerStatus.Invincible = true;
        yield return new WaitForSeconds(delay);
        Manager.Data.PlayerStatus.Invincible = false;
    }

    //public void TriggerPassiveEffects(PassiveTriggerType triggerType)
    //{
    //    if (accessories.Length <= 0) return;

    //    for (int i = 0; i < accessories.Length; i++)
    //    {
    //        if (accessories[i].Effect.passiveTriggerType == triggerType)
    //            accessories[i].Effect.Execute(accessories[i].itemName, accessories[i].UpgradeIdx);
    //    }
    //}
}
