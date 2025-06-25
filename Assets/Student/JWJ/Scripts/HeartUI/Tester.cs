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
        if (Input.GetKeyDown(KeyCode.Z)) //현재 체력 감소
        {
            currentHp--;
            healthHeart.HeartUpdate(currentHp);   //업데이트
            Debug.Log("체력 감소: " + currentHp);
        }

        if (Input.GetKeyDown(KeyCode.X)) //체력 증가
        {
            if (currentHp < maxHp)
            {
                currentHp++;
                Debug.Log("체력 회복: " + currentHp);
                healthHeart.HeartUpdate(currentHp);   //업데이트
            }
            else
                Debug.Log("최대체력입니다");
            
            
        }

        if (Input.GetKeyDown(KeyCode.C))  //최대체력 증가
        {
            maxHp++;
            currentHp++; 
            healthHeart.InicialHearts(maxHp); //하트리스트 초기화
            healthHeart.HeartUpdate(currentHp); //업데이트
            Debug.Log("최대체력 증가: " + maxHp);
        }

        if (Input.GetKeyDown(KeyCode.V))  //최대체력 감소
        {
            maxHp--;
            currentHp = Mathf.Min(currentHp, maxHp);

            healthHeart.InicialHearts(maxHp); //하트리스트 초기화 
            healthHeart.HeartUpdate(currentHp);  //업데이트
            Debug.Log("최대체력 감소: " + maxHp);
            Debug.Log("현재체력 :" + currentHp);
        }
    }

}

