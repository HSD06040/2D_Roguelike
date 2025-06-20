using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    private void Start()
    {
        base.Start();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
