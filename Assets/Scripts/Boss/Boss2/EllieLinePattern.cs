using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllieLinePattern : BossPattern
{
    [Header("Ratio")]
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    [Space]
    [SerializeField] private GameObject warningLine;
    [SerializeField] private float delay;

    private class WarningLineInfo
    {
        public Vector2 position;
        public float rotationZ;

        public WarningLineInfo(Vector2 pos, float rot)
        {
            position = pos;
            rotationZ = rot;
        }
    }
    
    private int verticalCount, horizontalCount;
    private int verticalRandom, horizontalRandom;

    private List<WarningLineInfo> lineInfos = new List<WarningLineInfo>();

    [Header("Angles")]
    [SerializeField] private float[] downAngle;
    [SerializeField] private float[] upAngle;
    [SerializeField] private float[] leftAngle;
    [SerializeField] private float[] rightAngle;

    protected override IEnumerator PatternRoutine()
    {
        Init();
        SetupLines();

        for (int i = 0; i < lineInfos.Count; i++)
        {
            Instantiate(warningLine, lineInfos[i].position, Quaternion.Euler(0,0,lineInfos[i].rotationZ + -90f)).GetComponent<WarningLine>().Init(duration);
            yield return Utile.GetDelay(interval);
        }

        yield return Utile.GetDelay(delay);

        for (int i = 0; i < lineInfos.Count; i++)
        {
            Instantiate(prefab, lineInfos[i].position, Quaternion.Euler(0, 0, lineInfos[i].rotationZ));
            yield return Utile.GetDelay(interval);
        }

        OnComplated?.Invoke();
    }

    private void SetupLines()
    {
        horizontalRandom = Random.Range(0, verticalCount);
        horizontalCount -= horizontalRandom;

        // 아래에서 위
        for (int i = 0; i < horizontalRandom; i++)
        {
            float x = Random.Range(minWidth, maxWidth);
            Vector2 pos = new Vector2(x, minHeight);
            lineInfos.Add(new WarningLineInfo(pos, Random.Range(downAngle[0], downAngle[1])));
        }

        // 위에서 아래
        for (int i = 0; i < horizontalCount; i++)
        {
            float x = Random.Range(minWidth, maxWidth);
            Vector2 pos = new Vector2(x, maxHeight);
            lineInfos.Add(new WarningLineInfo(pos, Random.Range(upAngle[0], upAngle[1])));
        }

        verticalRandom = Random.Range(0, horizontalCount);
        verticalCount -= verticalRandom;

        // 왼쪽에서 오른쪽
        for (int i = 0; i < verticalRandom; i++)
        {
            float y = Random.Range(minHeight, maxHeight);
            Vector2 pos = new Vector2(minWidth, y);
            lineInfos.Add(new WarningLineInfo(pos, Random.Range(leftAngle[0], leftAngle[1])));
        }

        for (int i = 0; i < verticalCount; i++)
        {
            float y = Random.Range(minHeight, maxHeight);
            Vector2 pos = new Vector2(maxWidth, y);
            lineInfos.Add(new WarningLineInfo(pos, Random.Range(rightAngle[0], rightAngle[1])));
        }
    }

    private void Init()
    {
        lineInfos.Clear();
        verticalCount = 6;
        horizontalCount = 6;
    }    
}
