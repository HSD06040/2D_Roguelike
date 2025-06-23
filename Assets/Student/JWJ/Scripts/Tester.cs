using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private PlayerStatusController playerStatusController;
    [SerializeField] private int healAmount;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            playerStatusController.Heal(healAmount);
            
        }
    }
}
