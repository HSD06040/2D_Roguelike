using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violin : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        Projectile obj = WeaponData.Projectile;
        obj.Init(mousePosition, WeaponData.AttackDamage, WeaponData.AttackSpeed);
    }
}
