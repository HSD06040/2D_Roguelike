using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_Test_Player : MonoBehaviour
{
    PlayerFight playerFight;
    public MusicWeapon Weapon;
    public MusicWeapon Weapon2;
    public MusicWeapon Weapon3;
    private void Start()
    {
        playerFight = GetComponent<PlayerFight>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerFight.AddMusicWeapon(Weapon);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerFight.AddMusicWeapon(Weapon2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerFight.AddMusicWeapon(Weapon3);
        }
    }

}
