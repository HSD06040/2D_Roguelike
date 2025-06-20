using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicWeapon", menuName = "MusicWeapon/Basic", order = 0)]
public class Weapon : Item
{
    public int power;
    public AnimatorOverrideController weaponOverride = null;
    public float attackRange;
    public float attackDelay;
    public float attackRadius;
    public int weaponMaxCount;
    public GameObject[] weaponParticle;



    public float GetDamage()
    {
        return power;
    }

    public float GetRange()
    {
        return attackRange;
    }

    public float GetRadius()
    {
        return attackRadius;
    }

    public float GetDelay()
    {
        return attackDelay;
    }
}
