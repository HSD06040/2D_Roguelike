using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_Test_Player : MonoBehaviour
{
    PlayerFight playerFight;
    public List<MusicWeapon> musicWeaponList = new();
    public MusicWeapon weapon;
    public MusicWeapon weapon2;
    public MusicWeapon weapon3;
    private void Start()
    {
        playerFight = GetComponent<PlayerFight>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerFight.AddMusicWeapon(weapon);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerFight.AddMusicWeapon(weapon2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerFight.AddMusicWeapon(weapon3);
        }
    }

}
