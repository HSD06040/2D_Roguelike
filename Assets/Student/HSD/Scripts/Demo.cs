using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private PlayerWeaponController controller;
    [SerializeField] private MusicWeapon[] weapons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Manager.Data.playerStatus.AddWeapon(MusicWeaponType.Gun);

        if (Input.GetKeyDown(KeyCode.X))
            Manager.Data.playerStatus.AddWeapon(MusicWeaponType.Trumpet);

        if (Input.GetKeyDown(KeyCode.C))
            Manager.Data.playerStatus.AddWeapon(MusicWeaponType.Violin);

        if (Input.GetKeyDown(KeyCode.V))
            Manager.Data.playerStatus.AddWeapon(MusicWeaponType.Cymbals);
    }
}
