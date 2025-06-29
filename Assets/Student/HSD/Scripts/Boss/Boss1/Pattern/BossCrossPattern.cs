using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrossPattern : BossPattern
{
    [SerializeField] private bool isRotate;
    [SerializeField] private GameObject warningLine;

    [SerializeField] private Transform pivot;
    [SerializeField] private Transform[] warningLinePoints;

    protected override IEnumerator PatternRoutine()
    {
        for (int i = 0; i < warningLinePoints.Length; i++)
        {
            Instantiate(warningLine, warningLinePoints[i].position, warningLinePoints[i].rotation, pivot).GetComponent<WarningLine>().Init(duration);
        }

        if (isRotate)
        {
            float elapsed = 0f;
            float rotateDuration = duration;
            float totalAngle = 90f;
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
    }
}
