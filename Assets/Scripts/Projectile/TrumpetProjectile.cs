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
        transform.right = _targetPos;
    }

    private void Start()
    {
        gameObject.transform.localScale = new Vector2(0.2f, 0.2f);
        data = Manager.Data.PassiveCon.orbitController.transform.position;
        maxValue = 20;
        rand = Random.Range(0, SFXAudioSound.Length);
        Debug.Log($"{rand}");

        if (sizeCor == null)
        {
            sizeCor = StartCoroutine(UpSizeCor(maxValue));
        }

        if (isStart)
        {
            if (SFXAudioSound[rand] == "")
                return;

            Manager.Audio.PlaySFX($"Weapon/{SFXAudioSound[rand]}", transform.position);
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
