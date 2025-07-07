using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Trumpet : MusicWeapon
{
    Coroutine weaponCor;
    WaitForSeconds delay = new WaitForSeconds(0.2f);


    public override void Attack(Vector2 mousePosition)
    {
        Vector2 pos = (mousePosition - (Vector2)player.position).normalized;
        if (weaponCor == null)
        {
            weaponCor = StartCoroutine(WeaponCor(pos));
        }
    }

    IEnumerator WeaponCor(Vector2 mousePosition)
    {
        Projectile obj0 = Instantiate(WeaponData.Projectiles[0], (Vector2)player.position + mousePosition * mouseOffset, Quaternion.identity);
        obj0.Init(mousePosition, curAttackDamage, WeaponData.AttackSpeed);
        yield return delay;
        Projectile obj1 = Instantiate(WeaponData.Projectiles[1], (Vector2)player.position + mousePosition * mouseOffset, Quaternion.identity);
        obj1.Init(mousePosition, curAttackDamage, WeaponData.AttackSpeed);
        yield return delay;
        Projectile obj2 = Instantiate(WeaponData.Projectiles[2], (Vector2)player.position + mousePosition * mouseOffset, Quaternion.identity);
        obj2.Init(mousePosition, curAttackDamage, WeaponData.AttackSpeed);

        yield return null;

        if (weaponCor != null)
        {
            StopCoroutine(weaponCor);
            weaponCor = null;
        }
    }
}
