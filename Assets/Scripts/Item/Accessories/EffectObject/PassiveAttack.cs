using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAttack : PassiveObject
{
    [SerializeField] private BoxCollider2D col;


    public void AttackStart() => col.enabled = true;
    public void AttackEnd() => col.enabled = false;
    public void Release() => Manager.Resources.Destroy(gameObject);
}
