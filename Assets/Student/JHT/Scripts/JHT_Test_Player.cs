using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_Test_Player : MonoBehaviour
{
    PlayerFight playerFight;
    public MusicWeapon Weapon;

    public MusicWeapon curWeapon;

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
        //if(Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    playerFight.AddMusicWeapon(Weapon2);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    playerFight.AddMusicWeapon(Weapon3);
        //}

        curWeapon = Weapon;
        //마우스 포인터
        if(curWeapon != null) 
         SetProjectile(curWeapon);
    }

    private void SetProjectile(MusicWeapon musicWeapon)
    {
        if(musicWeapon == null)
        {
            Debug.Log("player : musicWeapon null");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);

            Debug.Log($"player : {musicWeapon.name}");
            Debug.Log($"player : {mousePosition}");

            //musicWeapon.Attack(mousePosition - transform.position);
            musicWeapon.Attack(mousePosition);
            //else
            //{
            //    playerFight.GoAreaProjectile(musicWeapon, mousePosition);
            //}
        }
    }

}
