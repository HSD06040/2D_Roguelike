using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_Test_Player : MonoBehaviour
{
    PlayerFight playerFight;

    public MusicWeapon Gun;
    public MusicWeapon trumpet;
    public MusicWeapon curWeapon;

    Camera camera;
    private Vector3 mousePosition;

    private void Start()
    {
        playerFight = GetComponent<PlayerFight>();
        camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            curWeapon = playerFight.AddMusicWeapon(trumpet);

        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            curWeapon = playerFight.AddMusicWeapon(Gun);
        }
        
        if(curWeapon != null)
        {
            SetProjectile(curWeapon);
        }
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


            //musicWeapon.Attack(mousePosition - transform.position);
            //if(playerFight.WeaponList.Contains(musicWeapon.WeaponData.itemName))
            //{
            //    musicWeapon.Attack(musicWeapon.WeaponData, mousePosition);
            //}
            musicWeapon.Attack(mousePosition);
        }
    }
}
