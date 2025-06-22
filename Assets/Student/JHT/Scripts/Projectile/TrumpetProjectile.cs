using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetProjectile : Projectile
{

    private void Update()
    {

        SetTrumpet();

    }

    public void SetTrumpet()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);

        if (transform.position == targetPos)
        {
            Destroy(this.gameObject, 0.3f); // PooledObject.ReturnPool()ÀÚ¸®
        }
    }

}
