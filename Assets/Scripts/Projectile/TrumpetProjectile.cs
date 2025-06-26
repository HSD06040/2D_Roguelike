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
    public override void Init(Vector2 _targetPos, int _damage, float _speed)
    {
        base.Init(_targetPos, _damage, _speed);
        targetPos = _targetPos;
    }

    private void Start()
    {
        data = TestSingleton.JHT_TestInstance.playerWeapon.transform.position;
        maxValue = ((Vector2)targetPos - data).magnitude;

        if (sizeCor == null)
        {
            sizeCor = StartCoroutine(UpSizeCor(maxValue));
        }
    }

    private IEnumerator UpSizeCor(float _maxValue)
    {
        float t = 1 / maxValue;
        
        while(transform.localScale.x <= 2)
        {
            yield return delay;
            transform.localScale += new Vector3(t, t, 0);
        }

        if(sizeCor != null)
        {
            StopCoroutine(sizeCor);
            sizeCor = null;
        }
    }



}
