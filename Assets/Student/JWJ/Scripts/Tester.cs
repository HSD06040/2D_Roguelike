using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] HealthHeart healthHeart;
    [SerializeField] private PlayerStatusController playerStatus;
    [SerializeField] private int testDamage = 5;

    [SerializeField] int maxHp = 10;
    [SerializeField] private int currentHp = 10;

    private void Start()
    {
        healthHeart.InicialHearts(maxHp);
        healthHeart.HeartUpdate(currentHp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) //���� ü�� ����
        {
            currentHp--;
            healthHeart.HeartUpdate(currentHp);   //������Ʈ
            Debug.Log("ü�� ����: " + currentHp);
        }

        if (Input.GetKeyDown(KeyCode.F2)) //ü�� ����
        {
            if (currentHp < maxHp)
            {
                currentHp++;
                Debug.Log("ü�� ȸ��: " + currentHp);
                healthHeart.HeartUpdate(currentHp);   //������Ʈ
            }
            else
                Debug.Log("�ִ�ü���Դϴ�");
            
            
        }

        if (Input.GetKeyDown(KeyCode.F3))  //�ִ�ü�� ����
        {
            maxHp++;
            currentHp++; 
            healthHeart.InicialHearts(maxHp); //��Ʈ����Ʈ �ʱ�ȭ
            healthHeart.HeartUpdate(currentHp); //������Ʈ
            Debug.Log("�ִ�ü�� ����: " + maxHp);
        }

        if (Input.GetKeyDown(KeyCode.F4))  //�ִ�ü�� ����
        {
            maxHp--;
            currentHp = Mathf.Min(currentHp, maxHp);

            healthHeart.InicialHearts(maxHp); //��Ʈ����Ʈ �ʱ�ȭ 
            healthHeart.HeartUpdate(currentHp);  //������Ʈ
            Debug.Log("�ִ�ü�� ����: " + maxHp);
            Debug.Log("����ü�� :" + currentHp);
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerStatus.TakeDamage(testDamage);
            Debug.Log("����ü�� :" + currentHp);
        }
    }

}

