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
    [SerializeField] private GameObject[] weaponSlots = new GameObject[4];
    private GameObject currentWeapon;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        currentWeapon = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != null)
            {
                // �������� �Ǳ��� ����ȣ��;
            }

            else
            {
                // �⺻������ ����;
            }
        }

        WeaponSwitch();
    }

    public void AddWeapon(GameObject weaponPrefab)
    {
        for(int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null)
            {
                GameObject newWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
                weaponSlots[i] = newWeapon;
                Debug.Log(newWeapon.name + "��" + (i +1) + "�� ���Կ� �߰�");
                return;
            }
        }
        Debug.Log("���� ������ ���� á���ϴ�");
    }

    private void WeaponSwitch()
    {
        for (int i = 0; i < weaponSlots.Length; i++) //������ ����ŭ Ű �Ҵ�
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) //�ڵ� Ű �Ҵ�
            {
                SelectWeapon(i); //SelectWeapon�� �ŰԺ��� �Ѱ���
                Debug.Log((i + 1) + "�� �Ǳ� ����");
                break;
            } 
        }
    }

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];  //������ ���� = currentWeapon
            Debug.Log((index + 1) + "�� ���� �����. �̸�: " + currentWeapon.name);
        }
        else 
        {
            Debug.Log((index + 1)+" ������ �������");
        }


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
