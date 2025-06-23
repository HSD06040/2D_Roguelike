using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cymbals : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        Projectile obj = Instantiate(WeaponData.Projectile);
        obj.Init(mousePosition);
        obj.transform.position = mousePosition;
    }

}
