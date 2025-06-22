using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MusicWeapon
{
    public override void Attack(Vector3 mousePosition)
    {
        Projectile obj = Instantiate(WeaponData.Projectile);
        obj.Init(mousePosition);
    }

}
