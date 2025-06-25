using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] HealthHeart healthHeart;
    [SerializeField] int maxHp = 10;
    [SerializeField] private int currentHp = 10;

    private void Start()
    {
        healthHeart.InicialHearts(maxHp);
        healthHeart.HeartUpdate(currentHp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) //���� ü�� ����
        {
            currentHp--;
            healthHeart.HeartUpdate(currentHp);   //������Ʈ
            Debug.Log("ü�� ����: " + currentHp);
        }

        if (Input.GetKeyDown(KeyCode.X)) //ü�� ����
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

        if (Input.GetKeyDown(KeyCode.C))  //�ִ�ü�� ����
        {
            maxHp++;
            currentHp++; 
            healthHeart.InicialHearts(maxHp); //��Ʈ����Ʈ �ʱ�ȭ
            healthHeart.HeartUpdate(currentHp); //������Ʈ
            Debug.Log("�ִ�ü�� ����: " + maxHp);
        }

        if (Input.GetKeyDown(KeyCode.V))  //�ִ�ü�� ����
        {
            maxHp--;
            currentHp = Mathf.Min(currentHp, maxHp);

            healthHeart.InicialHearts(maxHp); //��Ʈ����Ʈ �ʱ�ȭ 
            healthHeart.HeartUpdate(currentHp);  //������Ʈ
            Debug.Log("�ִ�ü�� ����: " + maxHp);
            Debug.Log("����ü�� :" + currentHp);
        }
    }

}

