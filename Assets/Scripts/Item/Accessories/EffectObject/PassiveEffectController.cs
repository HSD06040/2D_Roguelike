using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Experimental.AI;

public class PassiveEffectController : MonoBehaviour
{
    private Coroutine invincibleRoutine;
    private static readonly Dictionary<string, Coroutine> effectCoroutineDic = new Dictionary<string, Coroutine>();

    public OrbitController orbitController;

    private Accessories[] accessories;

    private void Start()
    {
        accessories = Manager.Data.PlayerStatus.PlayerAccessories;

        orbitController = FindObjectOfType<OrbitController>();
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
        yield return Utile.GetDelay(delay);
        Manager.Data.PlayerStatus.Invincible = false;
    }

    public void StartSkillCoroutine(GameObject prefab, string key, float interval, int count, float delay, float damage, float radius)
    {
        Coroutine newCoroutine = StartCoroutine(DelayRoutine(prefab, interval, count, delay, damage, radius));
        effectCoroutineDic[key] = newCoroutine;
    }

    public void StopSkillCoroutine(string key)
    {
        if(effectCoroutineDic.ContainsKey(key))
        {
            StopCoroutine(effectCoroutineDic[key]);
        }
    }

    private IEnumerator DelayRoutine(GameObject prefab, float interval, int count, float delay, float damage, float radius)
    {
        yield return Utile.GetDelay(interval);

        while (true)
        {
            for (int i = 0; i < count; i ++)
            {
                Instantiate(prefab, orbitController.transform.position, Quaternion.identity).GetComponent<PassiveObject>().Init(damage, radius);
                yield return Utile.GetDelay(delay);
            }
            
            yield return Utile.GetDelay(interval);
        }
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
