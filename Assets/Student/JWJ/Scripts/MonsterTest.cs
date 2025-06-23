using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : StatusController
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StatusController target = collision.gameObject.GetComponent<StatusController>();
        if(target != null)
        {
            Attack(target);
        }
    }
}
