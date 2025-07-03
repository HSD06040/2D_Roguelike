using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicWeapon", menuName = "MusicWeapon/Basic", order = 0)]
public class Weapon : Item
{
    [Header("Weapon")]
    public MusicWeaponType Type;
    public float[] AttackDamage;
    public float AttackSpeed;
    public AnimatorOverrideController WeaponOverride = null;
    public float[] AttackDelay;
    public int WeaponMaxUpgrade = 4;
    public GameObject[] WeaponParticle;
    public Projectile Projectile = null;
    public Projectile[] Projectiles = null;  
}

public enum MusicWeaponType
{
    Cymbals = 0,
    Flute = 1,
    Gun = 2,
    Trumpet = 3,
    Violin = 4
}
