using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPattern : MonoBehaviour
{
    [SerializeField, Tooltip("���� �⺻ ������Ʈ")]
    protected GameObject prefab;
    [SerializeField, Tooltip("���� ������Ʈ�� �����Ǳ� ���� ��ٸ��� �ð�")] 
    protected float interval;
    [SerializeField, Tooltip("������ �Ǳ� ������ �ð�")]
    protected float duration;

    public virtual void Execute() => StartCoroutine(PatternRoutine());

    protected abstract IEnumerator PatternRoutine();
}
