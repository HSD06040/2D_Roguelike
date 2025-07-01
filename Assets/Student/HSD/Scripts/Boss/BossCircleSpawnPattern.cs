using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Transform, Player
}

public class BossCircleSpawnPattern : BossPattern
{
    [Header("Effect")]
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private Vector3 size;
    [Space]
    [SerializeField] private int count;
    [SerializeField] private Transform[] transforms;
    [SerializeField] private TargetType targetType;

    protected override IEnumerator PatternRoutine(Monster boss)
    {
        Instantiate(effect, boss.transform.position, Quaternion.identity);

        yield return Utile.GetDelay(duration);

        if(targetType == TargetType.Player)
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(prefab, fsm.Player.position, Quaternion.identity).GetComponent<BossPatternObject>().Setup(duration, attackEffect, size, true);
                yield return Utile.GetDelay(interval);
            }
        }
        else
        {
            for (int i = 0; i < transforms.Length; i ++)
            {
                Instantiate(prefab, transforms[i].position, Quaternion.identity).GetComponent<BossPatternObject>().Setup(duration, attackEffect, size, true);
                yield return Utile.GetDelay(interval);
            }
        }

        OnComplated?.Invoke();
    }
}
