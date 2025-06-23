using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CymbalsProjectile : Projectile
{
    [SerializeField] private SpriteRenderer cymbalsSprite;

    public float LivingTime;
    private float time = 0;
    private float changeShaderValue;


    public override void Init(Vector2 _targetPos){}

    private void Update()
    {
        time += Time.deltaTime;
        //changeShaderValue = time;
        float value = Mathf.Lerp(0, 1, changeShaderValue);
        cymbalsSprite.material.SetFloat("_WaveDistanceFromCenter", value);

        if(time > LivingTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //TakeDamage
        }
    }
}
