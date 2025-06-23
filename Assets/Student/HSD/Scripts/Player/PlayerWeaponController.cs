using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    MusicWeapon musicWeapon;
    Camera cam;

    public Transform[] WeaponSpawnPos;

    public List<string> WeaponList;
    private int weaponIdx = 0;

    [Header("무기")]
    [SerializeField] private MusicWeapon defaultWeapon;
    [SerializeField] private MusicWeapon[] weaponSlots = new MusicWeapon[4];
    private MusicWeapon currentWeapon;


    private void Start()
    {
        WeaponList = new List<string>(4);
        cam = Camera.main;
        defaultWeapon = GetComponentInChildren<MusicWeapon>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != null)
            {
                SetProjectile(currentWeapon);
            }

            else
            {
                defaultWeapon.Attack(GetMousePos());
            }
        }

        WeaponSwitch();
    }

    #region List<string>형식으로 weapon받아오기
    public MusicWeapon AddMusicWeapon(MusicWeapon _musicWeapon)
    {
        if (_musicWeapon == null) return null;
        int notSet = 0;

        if (!WeaponList.Contains(_musicWeapon.WeaponData.itemName))
        {
            WeaponList.Add(_musicWeapon.WeaponData.itemName);
            weaponSlots[weaponIdx] = _musicWeapon;
            weaponIdx++;
            notSet = WeaponList.Count - 1;

            MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[notSet]);
            //_musicWeapon.Spawn(WeaponSpawnPos[notSet]).GetOrAddComponent<MusicWeapon>();

            musicWeapon = weapon; // 새 무기 할당
            return weapon;
        }
        else
        {
            for (int i = 0; i < WeaponList.Count; i++)
            {
                if (WeaponList[i] == _musicWeapon.WeaponData.itemName)
                {
                    notSet = i;
                    break;
                }
            }
            // 기존 무기를 찾기 : WeaponSpawnPos에 자식으로 존재하는 무기
            Transform slot = WeaponSpawnPos[notSet];
            musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // 기존 무기 할당


            if (musicWeapon != null)
                CheckOldWeapon();
            else
                Debug.LogWarning("기존 무기를 찾을 수 없음.");

            return musicWeapon;
        }
    }
    #endregion
    public void CheckOldWeapon() // 파라미터 Player
    {
        musicWeapon.Count++;
        if (musicWeapon.Count > musicWeapon.WeaponData.WeaponMaxCount)
        {
            musicWeapon.Level++;
            musicWeapon.OnUpgrade?.Invoke(musicWeapon.Level);
            musicWeapon.Count = 0;
        }
    }
    private void WeaponSwitch()
    {
        for (int i = 0; i < weaponSlots.Length; i++) //슬롯의 수만큼 키 할당
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) //자동 키 할당
            {
                SelectWeapon(i); //SelectWeapon에 매게변수 넘겨줌
                Debug.Log((i + 1) + "번 악기 선택");
                break;
            }
        }
    }

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];  //선택한 무기 = currentWeapon
            Debug.Log((index + 1) + "번 무기 사용중. 이름: " + currentWeapon.name);
        }
        else
        {
            Debug.Log((index + 1) + " 슬롯은 비어있음");
        }
    }

    private void SetProjectile(MusicWeapon musicWeapon)
    {
        if (musicWeapon == null)
        {
            Debug.Log("player : musicWeapon null");
            return;
        }

        //musicWeapon.Attack((mousePosition - (Vector2)transform.position).normalized);
        musicWeapon.Attack(GetMousePos());
        //if(playerFight.WeaponList.Contains(musicWeapon.WeaponData.itemName))
        //{
        //    musicWeapon.Attack(musicWeapon.WeaponData, mousePosition);
        //}
        //musicWeapon.Attack(mousePosition);
    }

    private Vector2 GetMousePos()
    {
        Vector2 mPos = Input.mousePosition;
        return cam.ScreenToWorldPoint(mPos);        
    }
}
