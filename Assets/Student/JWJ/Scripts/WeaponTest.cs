using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject tesweaponPrefab;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            player.AddWeapon(tesweaponPrefab);
        }
    }
}
