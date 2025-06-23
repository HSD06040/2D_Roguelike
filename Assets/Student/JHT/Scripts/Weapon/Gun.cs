using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        Vector2 pos = mousePosition.normalized;
        Projectile obj = Instantiate(WeaponData.Projectile);
        obj.Init(pos);
    }

}
