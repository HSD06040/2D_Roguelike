using System;
using System.Collections;
using UnityEngine;

[Serializable]
public struct CrossInfo
{
    public float projectileSpeed;
    public float rotSpeed;
    public int dirCount;
    public bool isTotalAngleRandom;
    public float totalAngle;
    public float angleOffset;
    public float offset;
    public int count;
    public float duration;
    public float interval;
}

public class BossCrossPattern : BossPattern
{
    [SerializeField] private bool isRotate;
    [SerializeField] private GameObject warningLine;
    [SerializeField] private Transform pivot;
    [Header("Pattern Info")]
    [SerializeField] private CrossInfo[] crossInfos;
    private static readonly int[] randomAngles = { 0, 90, 180, 270 };
    private float temp;
    private float angleStep;

    private Coroutine rotateRoutine;

    protected override IEnumerator PatternRoutine(Monster boss)
    {
        yield return Utile.GetDelay(duration);

        for (int i = 0; i < crossInfos.Length; i++)
        {
            CrossInfo info = crossInfos[i];

            if (info.isTotalAngleRandom)
            {
                temp = info.totalAngle;
                info.totalAngle = 360;
                angleStep = info.totalAngle / (info.dirCount * 4);
            }
            else
                angleStep = info.totalAngle / info.dirCount;

            if(info.isTotalAngleRandom)
            {
                for (int j = 0; j < info.dirCount * 4; j++)
                {
                    float angle = angleStep * j + info.angleOffset;
                    Quaternion rot = Quaternion.Euler(0, 0, angle);
                    Vector2 direction = rot * pivot.right;
                    Vector3 spawnPos = pivot.position + (Vector3)(direction * info.offset);

                    var line = Instantiate(warningLine, spawnPos, rot, pivot);
                    line.GetComponent<WarningLine>().Init(info.duration / 2f);
                }
            }
            else
            {
                for (int j = 0; j < info.dirCount; j++)
                {
                    float angle = angleStep * j + info.angleOffset;
                    Quaternion rot = Quaternion.Euler(0, 0, angle);
                    Vector2 direction = rot * pivot.right;
                    Vector3 spawnPos = pivot.position + (Vector3)(direction * info.offset);

                    var line = Instantiate(warningLine, spawnPos, rot, pivot);
                    line.GetComponent<WarningLine>().Init(info.duration / 2f);
                }
            }
            

            if (isRotate)
            {
                StartCoroutine(RotateOverTime(info.duration, info.totalAngle));
            }

            if (info.isTotalAngleRandom)
            {
                info.totalAngle = temp;
                angleStep = info.totalAngle / info.dirCount;
            }

            yield return Utile.GetDelay(info.duration);

            if (isRotate)
            {
                rotateRoutine = StartCoroutine(PivotRotate(info.rotSpeed, info.interval * info.count));
            }

            for (int j = 0; j < info.count; j++)
            {
                Fire(boss, info);
                yield return Utile.GetDelay(info.interval);
            }

            if (isRotate && rotateRoutine != null)
                StopCoroutine(rotateRoutine);

            pivot.rotation = Quaternion.Euler(0, 0, 0);
        }

        OnComplated?.Invoke();
    }

    private IEnumerator RotateOverTime(float duration, float totalAngle)
    {
        float elapsed = 0f;
        float currentAngle = 0f;

        while (elapsed < duration)
        {
            float delta = Time.deltaTime;
            elapsed += delta;

            float t = Mathf.Clamp01(elapsed / duration);
            float targetAngle = Mathf.Lerp(0f, totalAngle, t);
            float step = targetAngle - currentAngle;

            pivot.Rotate(Vector3.forward, step);
            currentAngle = targetAngle;

            yield return null;
        }
    }

    private IEnumerator PivotRotate(float rotSpeed, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float delta = Time.deltaTime;
            pivot.Rotate(Vector3.forward, rotSpeed * delta);
            elapsed += delta;
            yield return null;
        }
    }

    private void Fire(Monster boss, CrossInfo info)
    {
        float angleStep = info.totalAngle / info.dirCount;

        if (info.isTotalAngleRandom)
        {
            info.angleOffset = randomAngles[UnityEngine.Random.Range(0, randomAngles.Length)];
        }

        for (int i = 0; i < info.dirCount; i++)
        {
            float angle = info.angleOffset + angleStep * i;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * pivot.right;

            GameObject projectile = Manager.Resources.Instantiate(prefab, pivot.transform.position, true);

            if (projectile != null)
            {
                projectile.GetComponent<Projectile_Controller>().Initialize(
                    direction.normalized,
                    info.projectileSpeed,
                    boss.AttackPower,
                    ""
                );
            }
        }
    }
}
