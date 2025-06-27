using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomAttackEffect", menuName = "Item/Accessories/Effect/RandomAttackEffect")]
public class ChanceAttackEffect : AccessoriesEffect
{
    [Header("ChanceAttack")]
    [SerializeField] private GameObject attacker;
    [SerializeField] private GameObject attack;
    [SerializeField] private float offset;
    [SerializeField, Range(0, 100)] private float[] chances;
    [SerializeField] private float[] damages;
    [SerializeField] private int[] monsterCounts;

    public override void Active1(Accessories accessories)
    {
        CreateAttacker(0);
    }

    public override void Active2(Accessories accessories)
    {
        CreateAttacker(1);
    }

    public override void Active3(Accessories accessories)
    {
        CreateAttacker(2);
    }

    public override void Active4(Accessories accessories)
    {
        CreateAttacker(3);
    }

    public override void Revoke(Accessories accessories)
    {
        
    }

    private void CreateAttacker(int _upgrade)
    {
        if (Random.Range(0,100) < chances[_upgrade])
        {
            Debug.Log("Spawn Attacker");
            Manager.Resources.Instantiate
            (attacker, Manager.Data.PassiveCon.orbitController.transform.position, Quaternion.identity)
            .GetComponent<EffectRandomAttacker>().Init(monsterCounts[_upgrade], damages[_upgrade], attack, offset);
        }        
    }
}
