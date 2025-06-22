using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : Projectile
{

    private void Update()
    {

        SetGun();

    }

    public void SetGun()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);

        if (transform.position == targetPos)
        {
            Destroy(this.gameObject, 0.3f); // PooledObject.ReturnPool()ÀÚ¸®
        }
    }
}
