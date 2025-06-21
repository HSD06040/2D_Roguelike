using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MusicWeapon
{


    public override void Attack(Vector3 mousePosition)
    {
        GameObject obj = Instantiate(WeaponData.Projectile);
        obj.GetComponent<Projectile>().Init(mousePosition);
    }
}
