using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaProjectile : MonoBehaviour
{
    [SerializeField] private int damage;
    StatusController target = null;

    [SerializeField] private float maxTime;
    [SerializeField] private GameObject bulletModel;
    [SerializeField] private float Speed = 1;
    private Rigidbody rigid;
    public bool IsPass;

    private Vector3 targetPos;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(Transform _projectilePos, Vector3 _targetPos)
    {
        GameObject obj = Instantiate(bulletModel, _projectilePos);
        targetPos = _targetPos;
        obj.transform.position = targetPos;
    }


        
    
}
