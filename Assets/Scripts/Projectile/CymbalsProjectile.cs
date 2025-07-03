using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CymbalsProjectile : Projectile
{
    [SerializeField] private SpriteRenderer cymbalsSprite;
    
    private float time = 0;    
    private float speed;

    public override void Init(Vector2 _targetPos, float _damage, float _speed)
    { 
        damage = _damage; 
        speed = _speed;
    }

    private void Update()
    {
        time += Time.deltaTime * speed;
        float value = Mathf.Lerp(0, 1, time);        
        cymbalsSprite.material.SetFloat("_WaveDistanceFromCenter", value);

        if(1 == cymbalsSprite.material.GetFloat("_WaveDistanceFromCenter"))
        {
            Destroy(gameObject);
        }
    }
}
