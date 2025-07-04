using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossTargetLock : BossPattern
{
    [Header("TargetLock")]
    [SerializeField] private GameObject warningLine;
    [SerializeField] private int count;
    [SerializeField] private float deleteDelay;
    [SerializeField] private float laserDelay;

    protected override IEnumerator PatternRoutine()
    {
        for (int i = 0; i < count; i++)
        {
            yield return Utile.GetDelay(duration);
            Vector2 dir = fsm.Player.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(warningLine, transform.position, targetRotation).GetComponent<WarningLine>().Init(duration);

            yield return Utile.GetDelay(interval);

            targetRotation = Quaternion.Euler(0f, 0f, angle -90);
            Instantiate(prefab, transform.position, targetRotation).GetComponent<EllieLine>().Init(deleteDelay, laserDelay);
        }

        OnComplated?.Invoke();
    }
}
