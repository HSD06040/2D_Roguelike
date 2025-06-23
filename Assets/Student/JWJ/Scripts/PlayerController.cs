using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{  //�����̽��� �뽬 �����ؾ���

    [Header("�÷��̾�")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rigid;
    private Vector2 movemoent;
    Camera cam;

    [Header("����")]
    [SerializeField] private GameObject defaultWeapon;
    private List<GameObject> ownedWeapons = new List<GameObject>(); //ȹ���� �Ǳ� ����Ʈ
    private GameObject currentWeapon;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != null)
            {
                // �������� �Ǳ��� ����;
            }

            else
            {
                // �⺻������ ����;
            }
        }

        WeaponSwap();
    }

    public void AddWeapon(GameObject weaponPrefab)
    {
        GameObject newWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        ownedWeapons.Add(newWeapon);
        Debug.Log("�����߰�" + newWeapon.name);
    }

    private void WeaponSwap()
    {
        for (int i = 0; i < ownedWeapons.Count; i++) //������ �Ǳ��� ������ŭ Ű �Ҵ�
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) //�ڵ� Ű �Ҵ�
            {
                SelectWeapon(i); //SelectWeapon�� �ŰԺ��� �Ѱ���
                Debug.Log((i + 1) + "�� �Ǳ� ����");
            } 
        }

        if (ownedWeapons.Count == 0) //���Ⱑ ������ currentWeapon = null ó���ؼ� �⺻���� ���
        {
            currentWeapon = null;
        }
    }

    private void SelectWeapon(int index)
    {
        if (index >= ownedWeapons.Count)  //���������� ū ������ Ű�� ������ ����
        {
            return;
        }

        currentWeapon = ownedWeapons[index];  //������ ���� = currentWeapon
        Debug.Log((index +1) + "�� ���� �����");

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()  //�÷��̾� �̵� �Լ�
    {
        movemoent.x = Input.GetAxisRaw("Horizontal");
        movemoent.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = movemoent.normalized * moveSpeed;
    }

}
