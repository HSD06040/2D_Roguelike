using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_Test : MonoBehaviour
{
    [SerializeField] private JHT_PlayerWeapon controller;
    [SerializeField] private MusicWeapon[] weapons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            controller.AddMusicWeapon(weapons[0]);

        if (Input.GetKeyDown(KeyCode.X))
            controller.AddMusicWeapon(weapons[1]);

        if (Input.GetKeyDown(KeyCode.C))
            controller.AddMusicWeapon(weapons[2]);

        if (Input.GetKeyDown(KeyCode.V))
            controller.AddMusicWeapon(weapons[3]);
    }
}
