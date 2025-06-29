using System.Collections;
using UnityEngine;

public class BossCrossPattern : BossPattern
{
    [SerializeField] private bool isRotate;
    [SerializeField] private GameObject warningLine;
    [SerializeField] private Transform pivot;

    [Header("Pattern Info")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float dirCount;
    [SerializeField] private float angleOffset;
    [SerializeField] private float offset;
    [SerializeField] private int count;
    private Coroutine rotateRoutine;
    float angleStep;

    protected override IEnumerator PatternRoutine(Monster boss)
    {
        angleStep = 360 / dirCount;

        for (int i = 0; i < dirCount; i++)
        {
            float angle = angleStep + angleOffset * i;

            Vector2 direction = Quaternion.Euler(0, 0, angle) * pivot.up;
            Vector3 spawnPos = pivot.position + (Vector3)(direction * offset);
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            var line = Instantiate(warningLine, spawnPos, rot, pivot);
            line.GetComponent<WarningLine>().Init(duration/2);
        }

        if (isRotate)
        {
            StartCoroutine(Rotate());
        }

        yield return Utile.GetDelay(duration);

        if (isRotate)
        {
            rotateRoutine = StartCoroutine(PivotRotate());
        }


        for (int i = 0; i < count; i++)
        {
            Fire(boss);
            yield return Utile.GetDelay(interval);
        }

        if(isRotate)
            StopCoroutine(rotateRoutine);

        pivot.rotation = Quaternion.Euler(0,0,0);
    }

    private IEnumerator Rotate()
    {
        float elapsed = 0f;
        float rotateDuration = duration;
        float totalAngle = angleStep;
        float currentAngle = 0f;

        while (elapsed < rotateDuration)
        {
            float delta = Time.deltaTime;
            elapsed += delta;

            float t = Mathf.Clamp01(elapsed / rotateDuration);
            float targetAngle = Mathf.Lerp(0f, totalAngle, t);

            float step = targetAngle - currentAngle;
            pivot.Rotate(Vector3.forward, step);

            currentAngle = targetAngle;
            yield return null;
        }
    }

    private IEnumerator PivotRotate()
    {
        float elapsed = 0f;
        float rotateDuration = interval * count;

        while (elapsed < rotateDuration)
        {
            float delta = Time.deltaTime;
            float step = rotSpeed * delta;
            pivot.Rotate(Vector3.forward, step);
            elapsed += delta;

            yield return null;
        }
    }

    private void Fire(Monster boss)
    {
        for (int i = 0; i < dirCount; i++)
        {
            float angle = angleOffset + angleStep * i;

            Vector2 direction = Quaternion.Euler(0, 0, angle) * pivot.right;

            GameObject projectile = Manager.Resources.Instantiate(prefab, boss.transform.position, true);

            if (projectile != null)
            {
                projectile.GetComponent<Projectile_Controller>().Initialize(
                    direction.normalized,
                    projectileSpeed,
                    boss.AttackPower,
                    ""
                );
            }
        }
    }
}
