using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicWeapon", menuName = "MusicWeapon/Basic", order = 0)]
public class Weapon : Item
{
    public MusicWeaponType Type;
    public int AttackDamage;
    public float AttackSpeed;
    public AnimatorOverrideController WeaponOverride = null;
    public float AttackRange;
    public float AttackDelay { get; private set;}
    public float AttackRadius;
    public int WeaponMaxCount;
    public GameObject[] WeaponParticle;
    public Projectile Projectile = null;
    public Projectile[] Projectiles = null;
    
}

public enum MusicWeaponType
{
    Gun,
    Trumpet,
    Cymbals
}
