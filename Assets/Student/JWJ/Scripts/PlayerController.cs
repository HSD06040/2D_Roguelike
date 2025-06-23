using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{  //스페이스바 대쉬 구현해야함

    [Header("플레이어")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rigid;
    private Vector2 movemoent;
    Camera cam;

    [Header("무기")]
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
                // 선택중인 악기의 공격호출;
            }

            else
            {
                // 기본무기의 공격;
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
                Debug.Log(newWeapon.name + "를" + (i +1) + "번 슬롯에 추가");
                return;
            }
        }
        Debug.Log("무기 슬롯이 가득 찼습니다");
    }

    private void WeaponSwitch()
    {
        for (int i = 0; i < weaponSlots.Length; i++) //슬롯의 수만큼 키 할당
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) //자동 키 할당
            {
                SelectWeapon(i); //SelectWeapon에 매게변수 넘겨줌
                Debug.Log((i + 1) + "번 악기 선택");
                break;
            } 
        }
    }

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];  //선택한 무기 = currentWeapon
            Debug.Log((index + 1) + "번 무기 사용중. 이름: " + currentWeapon.name);
        }
        else 
        {
            Debug.Log((index + 1)+" 슬롯은 비어있음");
        }


    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()  //플레이어 이동 함수
    {
        movemoent.x = Input.GetAxisRaw("Horizontal");
        movemoent.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = movemoent.normalized * moveSpeed;
    }

}
