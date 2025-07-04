using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Experimental.AI;

public class PassiveEffectController : MonoBehaviour
{
    private Coroutine invincibleRoutine;
    private static readonly Dictionary<string, Coroutine> effectCoroutineDic = new Dictionary<string, Coroutine>();

    private OrbitController _orbitController;
    public OrbitController orbitController
    {
        get
        {
            if (_orbitController == null)
                _orbitController = FindObjectOfType<OrbitController>();

            return _orbitController;
        }
    }

    private Accessories[] accessories;

    private void Start()
    {
        accessories = Manager.Data.PlayerStatus.PlayerAccessories;        
    }

    public void PlayerInvincible(float invincibleDuration, float interval, string key, GameObject effectPrefab = null)
    {
        if (effectCoroutineDic.ContainsKey(key))
        {
            StopCoroutine(effectCoroutineDic[key]);
            effectCoroutineDic.Remove(key);
        }

        Coroutine routine = StartCoroutine(PlayerInvincibleLoopRoutine(invincibleDuration, interval, effectPrefab));
        effectCoroutineDic.Add(key, routine);
    }

    private IEnumerator PlayerInvincibleLoopRoutine(float invincibleDuration, float interval, GameObject effectPrefab)
    {
        while (true)
        {
            Manager.Data.PlayerStatus.Invincible = true;

            if (effectPrefab != null)
            {
                GameObject fx = Instantiate(effectPrefab, orbitController.transform.position, Quaternion.identity);
                fx.transform.SetParent(orbitController.transform);
            }

            yield return Utile.GetDelay(invincibleDuration);

            Manager.Data.PlayerStatus.Invincible = false;

            yield return Utile.GetDelay(interval - invincibleDuration);
        }
    }

    public void StopPlayerInvincible(string key)
    {
        if (effectCoroutineDic.ContainsKey(key))
        {
            StopCoroutine(effectCoroutineDic[key]);
            effectCoroutineDic.Remove(key);
        }

        Manager.Data.PlayerStatus.Invincible = false;
    }

    public void StartSkillCoroutine(GameObject prefab, string key, float interval, int count, float delay, float damage, float radius)
    {
        Coroutine newCoroutine = StartCoroutine(DelayRoutine(prefab, interval, count, delay, damage, radius));
        effectCoroutineDic.Add(key, newCoroutine);
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
}
