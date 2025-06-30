using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPattern : MonoBehaviour
{
    [SerializeField, Tooltip("패턴 기본 오브젝트")]
    protected GameObject prefab;
    [SerializeField, Tooltip("다음 오브젝트가 생성되기 까지 기다리는 시간")] 
    protected float interval;
    [SerializeField, Tooltip("공격이 되기 까지의 시간")]
    protected float duration;
    [SerializeField] protected Monster Boss;
    [SerializeField] protected BossMonsterFSM fsm;

    public virtual void Execute() => StartCoroutine(PatternRoutine(Boss));

    protected abstract IEnumerator PatternRoutine(Monster boss);
}
