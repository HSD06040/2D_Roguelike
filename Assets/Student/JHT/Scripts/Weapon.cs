using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicWeapon", menuName = "MusicWeapon/Basic", order = 0)]
public class Weapon : Item
{
    public int AttackDamage;
    public AnimatorOverrideController WeaponOverride = null;
    public float AttackRange;
    public float AttackDelay { get; private set;}
    public float AttackRadius;
    public int WeaponMaxCount;
    public GameObject[] WeaponParticle;


}
