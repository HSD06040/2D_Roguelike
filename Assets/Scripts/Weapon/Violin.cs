using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violin : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        Projectile obj = Instantiate(WeaponData.Projectile, (Vector2)player.position + mousePosition * mouseOffset, Quaternion.identity);
        obj.Init(mousePosition, curAttackDamage, WeaponData.AttackSpeed);
    }
}