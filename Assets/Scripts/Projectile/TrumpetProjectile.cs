using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrumpetProjectile : Projectile
{
    private Vector2 data;
    private float maxValue;
    Vector2 lerpValue;

    private Coroutine sizeCor;
    private WaitForSeconds delay = new WaitForSeconds(0.2f);
    public override void Init(Vector2 _targetPos, float _damage, float _speed)
    {
        base.Init(_targetPos, _damage, _speed);
        targetPos = _targetPos;
        transform.right = targetPos.normalized;
    }

    private void Start()
    {
        gameObject.transform.localScale = new Vector2(0.2f, 0.2f);
        data = Manager.Data.PassiveCon.orbitController.transform.position;
        maxValue = 20;

        if (sizeCor == null)
        {
            sizeCor = StartCoroutine(UpSizeCor(maxValue));
        }

    }

    private IEnumerator UpSizeCor(float _maxValue)
    {
        float t = 4 / _maxValue;
        
        while(transform.localScale.x <= 1)
        {
            yield return delay;
            transform.localScale += new Vector3(t, t, 0);
        }
    }



}
