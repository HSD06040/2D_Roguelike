using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    private Camera cam;

    public Transform[] WeaponSpawnPos;

    [Header("����")]
    [SerializeField] private MusicWeapon defaultWeapon;
    private MusicWeapon[] weaponSlots;
    private MusicWeapon currentWeapon;


    private void Start()
    {
        cam = Camera.main;
        defaultWeapon = GetComponentInChildren<MusicWeapon>();
        defaultWeapon.Init(transform);
        weaponSlots = Manager.Data.PlayerStatus.PlayerWeapons;       
    }

    private void OnEnable()
    {
        Manager.Data.PlayerStatus.OnChangedWeapon += AddMusicWeapon;
        Manager.Data.PlayerStatus.OnCurrentWeaponChanged += WeaponSwitch;
        Manager.Input.GetPlayerBind("Attack").AddStartedEvent(Attack);
    }

    private void OnDisable()
    {
        Manager.Data.PlayerStatus.OnChangedWeapon -= AddMusicWeapon;
        Manager.Data.PlayerStatus.OnCurrentWeaponChanged -= WeaponSwitch;
        Manager.Input.GetPlayerBind("Attack").RemoveStartedEvent(Attack);
    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        if (currentWeapon != null)
        {            
            SetProjectile(currentWeapon);
            if (currentWeapon.WeaponData.itemName == "Violin")
            {
                Manager.Game.IsPress = true;
            }
        }
        else
        {
            defaultWeapon.Attack(GetMousePos());
        }

        Manager.Game.OnPlayerAttack?.Invoke();
    }

    #region List<string>�������� weapon�޾ƿ���
    public void AddMusicWeapon(int idx, MusicWeapon _musicWeapon)
    {
        if (_musicWeapon == null) return;

        MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[idx]);        
        weaponSlots[idx] = weapon;
        weapon.Init(transform);
    }
    #endregion

    private void WeaponSwitch(int _idx) => currentWeapon = weaponSlots[_idx];    

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];  //������ ���� = currentWeapon
            Debug.Log((index + 1) + "�� ���� �����. �̸�: " + currentWeapon.name);
        }
        else
        {
            Debug.Log((index + 1) + " ������ �������");
        }
    }

    private void SetProjectile(MusicWeapon musicWeapon)
    {
        if (musicWeapon == null)
        {
            Debug.Log("player : musicWeapon null");
            return;
        }

        musicWeapon.Attack(GetMousePos());
    }

    private Vector2 GetMousePos() => Manager.Input.GetMousePosition();
}
