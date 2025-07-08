using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flute : MusicWeapon
{
    public override void Attack(Vector2 mousePosition)
    {
        int rand = Random.Range(0, WeaponData.Projectiles.Length);
        Vector2 pos = (mousePosition - (Vector2)player.position).normalized;
        Projectile obj = Instantiate(WeaponData.Projectiles[rand], (Vector2)player.position + pos * mouseOffset, Quaternion.identity);
        obj.Init(pos, curAttackDamage, WeaponData.AttackSpeed);
    }
}
