using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoubleShot : BossPattern
{
    [Header("DoubleShot")]
    [SerializeField, Tooltip("총 몇번 발사할지")] private int count;
    [SerializeField, Tooltip("몇 점사일지")] private int burstCount;
    [SerializeField, Tooltip("퍼지는 간격")] private float spacing;
    [SerializeField] private float projectileInterval;
    [SerializeField] private float speed;
    private Vector3 target => fsm.Player.position;

    protected override IEnumerator PatternRoutine()
    {
        yield return Utile.GetDelay(duration);

        for (int i = 0; i < count; i++)
        {
            for (int j =0; j < burstCount; j++)
            {
                for (int k =-1; k <= 1; k += 2)
                {
                    Vector2 dir = (target - transform.position).normalized;

                    Vector2 perpendicular = new Vector2(-dir.y, dir.x);

                    Vector2 offset = perpendicular * spacing * k;

                    Instantiate(prefab, (Vector2)transform.position + offset, Quaternion.identity)
                        .GetComponent<Projectile_Controller>().Initialize(dir, speed, 1, "");
                }
                
                yield return Utile.GetDelay(projectileInterval);
            }
            yield return Utile.GetDelay(interval);
        }
    }    
}
