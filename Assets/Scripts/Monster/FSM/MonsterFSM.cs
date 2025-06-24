using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    public Monster Owner { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Transform Player { get; private set; }

    protected virtual void Awake()
    {
        Owner = GetComponent<Monster>();
        StateMachine = new StateMachine();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트가 없습니다");
        }
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }

    public float GetSqrDistanceToPlayer()
    {
        if (Player == null) return float.MaxValue;
        return (Player.position - transform.position).sqrMagnitude;
    }
}
