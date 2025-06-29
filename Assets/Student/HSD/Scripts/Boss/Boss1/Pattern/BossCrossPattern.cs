using System.Collections;
using UnityEngine;

public class BossCrossPattern : BossPattern
{
    [SerializeField] private bool isRotate;
    [SerializeField] private GameObject warningLine;

    [SerializeField] private Transform pivot;
    [SerializeField] private Transform[] warningLinePoints;
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private int count;
    private Coroutine rotateRoutine;
    float angleStep;

    protected override IEnumerator PatternRoutine(Monster boss)
    {
        for (int i = 0; i < warningLinePoints.Length; i++)
        {
            Instantiate(warningLine, warningLinePoints[i].position, warningLinePoints[i].rotation, pivot).GetComponent<WarningLine>().Init(duration);
        }

        if (isRotate)
        {
            StartCoroutine(Rotate());
        }

        yield return Utile.GetDelay(duration);

        angleStep = 360 / warningLinePoints.Length;

        if (isRotate)
        {
            rotateRoutine = StartCoroutine(PivotRotate());
        }


        for (int i = 0; i < count; i++)
        {
            Fore(boss);
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

    private void Fore(Monster boss)
    {
        for (int j = 0; j < warningLinePoints.Length; j++)
        {
            float currentAngle = angleStep * j;

            Vector2 centerDirection = pivot.transform.right * 1;
            centerDirection = Quaternion.Euler(0, 0, 90) * centerDirection;

            Vector2 fireDirection = Quaternion.Euler(0, 0, currentAngle) * centerDirection;

            GameObject projectile = Manager.Resources.Instantiate(prefab, boss.transform.position, true);

            Debug.Log($"{fireDirection.normalized} , {centerDirection} , {currentAngle}");
            if (projectile != null)
            {
                projectile.GetComponent<Projectile_Controller>().Initialize(
                    fireDirection.normalized,
                    speed,
                    boss.AttackPower,
                    ""
                    );
            }
        }
    }
}
