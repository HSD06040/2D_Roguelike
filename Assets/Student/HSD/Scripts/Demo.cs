using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private PlayerWeaponController controller;
    [SerializeField] private Accessories[] Accessories;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Manager.Data.PlayerStatus.AddWeapon(MusicWeaponType.Gun);

        if (Input.GetKeyDown(KeyCode.X))
            Manager.Data.PlayerStatus.AddWeapon(MusicWeaponType.Trumpet);

        if (Input.GetKeyDown(KeyCode.C))
            Manager.Data.PlayerStatus.AddWeapon(MusicWeaponType.Violin);

        if (Input.GetKeyDown(KeyCode.V))
            Manager.Data.PlayerStatus.AddWeapon(MusicWeaponType.Cymbals);

        if (Input.GetKeyDown(KeyCode.Q))
            Manager.Data.PlayerStatus.TryEquipAccessories(Accessories[Random.Range(0, Accessories.Length)]);
    }
}
