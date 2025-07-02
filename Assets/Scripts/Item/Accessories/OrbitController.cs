using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [SerializeField] private int objCount;
    [SerializeField] private float radius;
    [SerializeField] private float rotSpeed;
    private float angle;
    [SerializeField] private GameObject[] objs;

    public void Init(GameObject[] _objs)
    {
        objs = _objs;
    }

    private void Update()
    {
        if(objs.Length == 0) return;

        for (int i = 0; i < objs.Length; i++)
        {
            angle = (Time.time * rotSpeed + i * (360f / objs.Length)) * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            objs[i].transform.position = transform.position + offset;
        }        
    }
}
