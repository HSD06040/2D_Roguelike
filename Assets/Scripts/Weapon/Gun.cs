using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MusicWeapon
{

    public override void Attack(Vector2 mousePosition)
    {        
        Vector2 pos = (mousePosition - (Vector2)player.position).normalized;
        Projectile obj = Instantiate(WeaponData.Projectile, player.position, Quaternion.identity);
        obj.Init(pos, curAttackDamage, WeaponData.AttackSpeed);
    }
}
