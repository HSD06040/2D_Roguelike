using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cymbals : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        Projectile projectile = Instantiate(WeaponData.Projectile, mousePosition, Quaternion.identity);
        projectile.Init(mousePosition, WeaponData.AttackDamage, WeaponData.AttackSpeed);
    }
}
