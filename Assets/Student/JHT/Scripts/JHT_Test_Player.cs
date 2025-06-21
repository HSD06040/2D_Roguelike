using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_Test_Player : MonoBehaviour
{
    PlayerFight playerFight;
    public MusicWeapon Weapon;
    public MusicWeapon Weapon2;
    public MusicWeapon Weapon3;

    Camera camera;
    private Vector3 mousePosition;
    private void Start()
    {
        playerFight = GetComponent<PlayerFight>();
        camera = Camera.main.GetComponent<Camera>();
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

        //마우스 포인터
        SetProjectile(Weapon2);
    }

    private void SetProjectile(MusicWeapon musicWeapon)
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);

            if (musicWeapon == Weapon)
            {
                playerFight.GoProjectile(musicWeapon, mousePosition);
            }
            else
            {
                playerFight.GoAreaProjectile(musicWeapon, mousePosition);
            }
        }
    }

}
