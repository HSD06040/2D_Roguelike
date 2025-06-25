using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumpet : MusicWeapon
{
    Coroutine weaponCor;
    WaitForSeconds delay = new WaitForSeconds(0.2f);

    public override void Attack(Vector2 mousePosition)
    {
        Vector2 pos = (mousePosition - (Vector2)player.position).normalized;
        if (weaponCor == null)
        {
            Debug.Log($"{gameObject.name}");
            Debug.Log($"{gameObject.activeSelf}");
            weaponCor = StartCoroutine(WeaponCor(pos));
        }
    }

    IEnumerator WeaponCor(Vector3 mousePosition)
    {
        Projectile obj0 = Instantiate(WeaponData.Projectiles[0], player.position, Quaternion.identity);
        obj0.Init(mousePosition, WeaponData.AttackDamage, WeaponData.AttackSpeed);
        yield return delay;
        Projectile obj1 = Instantiate(WeaponData.Projectiles[1], player.position, Quaternion.identity);
        obj1.Init(mousePosition, WeaponData.AttackDamage, WeaponData.AttackSpeed);
        yield return delay;
        Projectile obj2 = Instantiate(WeaponData.Projectiles[2], player.position, Quaternion.identity);
        obj2.Init(mousePosition, WeaponData.AttackDamage, WeaponData.AttackSpeed);

        yield return null;

        if (weaponCor != null)
        {
            StopCoroutine(weaponCor);
            weaponCor = null;
        }
    }
}
