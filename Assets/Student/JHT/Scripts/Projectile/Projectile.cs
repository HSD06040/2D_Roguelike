using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Projectile : MonoBehaviour
{
    private int damage = 0;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed = 3;
    public bool IsPass;

    protected Vector3 targetPos;


    public void Init(Vector3 _targetPos)
    {
        targetPos = _targetPos;
    }




    //�ٽ�

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject == null) return; // Ʈ���� �ż� ��������� ���ÿ� Destroy�� ȣ��ɰ��
    //    if (other.GetComponent<StatusController>() != target && other.gameObject.layer != 7) //layer == 7 �÷��̾�� ����
    //        return;
    //    if (target == null) return; //�׾������ return;
    //
    //    target.TakeDamage(damage);
    //
    //    if (IsPass == true) return;
    //    Destroy(gameObject, maxTime);
    //    
    //    //particle
    //}
}
