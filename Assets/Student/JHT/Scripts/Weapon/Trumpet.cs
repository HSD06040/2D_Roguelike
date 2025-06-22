using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumpet : MusicWeapon
{
    Coroutine weaponCor;
    WaitForSeconds delay = new WaitForSeconds(0.2f);

    public override void Attack(Vector3 mousePosition)
    {
        if (weaponCor == null)
        {
            weaponCor = StartCoroutine(WeaponCor(mousePosition));
        }
    }

    // Coroutine Error°¡ ¶ß´Â ÄÚµå
    IEnumerator WeaponCor(Vector3 mousePosition)
    {
        Projectile obj0 = Instantiate(WeaponData.Projectiles[0]);
        obj0.Init(mousePosition);
        yield return delay;
        Projectile obj1 = Instantiate(WeaponData.Projectiles[1]);
        obj1.Init(mousePosition);
        yield return delay;
        Projectile obj2 = Instantiate(WeaponData.Projectiles[2]);
        obj2.Init(mousePosition);

        yield return null;

        if (weaponCor != null)
        {
            StopCoroutine(weaponCor);
            weaponCor = null;
        }
    }
}
