using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violin : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        Projectile obj = Instantiate(WeaponData.Projectile);
        obj.Init(mousePosition);
    }
}
