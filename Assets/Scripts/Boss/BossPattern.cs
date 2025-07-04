using System;
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
    [SerializeField] protected Monster boss;
    [SerializeField] protected BossMonsterFSM fsm;
    public Action OnComplated;
    private Coroutine pattern;

    public virtual void Execute() => pattern = StartCoroutine(PatternRoutine());
    public void Stop() => StopCoroutine(pattern);

    protected abstract IEnumerator PatternRoutine();
}
