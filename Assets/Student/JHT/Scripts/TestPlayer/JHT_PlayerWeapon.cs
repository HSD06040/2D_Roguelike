using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_PlayerWeapon : MonoBehaviour
{
    MusicWeapon musicWeapon;
    Camera cam;

    public Transform[] WeaponSpawnPos;

    public List<string> WeaponList;
    private int weaponIdx = 0;

    [Header("무기")]
    [SerializeField] private MusicWeapon defaultWeapon;
    [SerializeField] private MusicWeapon[] weaponSlots = new MusicWeapon[4];
    public MusicWeapon CurrentWeapon;


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
            if (CurrentWeapon != null)
            {
                SetProjectile(CurrentWeapon);
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
        int weaponNum = 0;

        if (!WeaponList.Contains(_musicWeapon.WeaponData.itemName))
        {
            //기존에 무기가 WeaponList에 없었을경우 WeaponList<string>에 넣어줌
            WeaponList.Add(_musicWeapon.WeaponData.itemName);
            weaponNum = WeaponList.Count - 1;

            MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[weaponNum]);

            weaponSlots[weaponIdx] = weapon;
            weaponIdx++;
            return musicWeapon;
        }
        else
        {

            //기존에 무기가 WeaponList에 있었을경우 count,level을 통해 다음 파티클 생성
            for (int i = 0; i < WeaponList.Count; i++)
            {
                if (WeaponList[i] == _musicWeapon.WeaponData.itemName)
                {
                    weaponNum = i;
                    break;
                }
            }

            Transform slot = WeaponSpawnPos[weaponNum];
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

    //UPdate에 자꾸돌아감
    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //자동 키 할당
        {
            SelectWeapon(1); //SelectWeapon에 매게변수 넘겨줌
            Debug.Log(1 + "번 악기 선택");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //자동 키 할당
        {
            SelectWeapon(2); //SelectWeapon에 매게변수 넘겨줌
            Debug.Log(2 + "번 악기 선택");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //자동 키 할당
        {
            SelectWeapon(3); //SelectWeapon에 매게변수 넘겨줌
            Debug.Log(3 + "번 악기 선택");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //자동 키 할당
        {
            SelectWeapon(4); //SelectWeapon에 매게변수 넘겨줌
            Debug.Log(4 + "번 악기 선택");
        }
    }

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index - 1] != null)
        {
            CurrentWeapon = weaponSlots[index - 1];  //선택한 무기 = currentWeapon
            Debug.Log(weaponSlots[index - 1].WeaponData.itemName + "번 무기 사용중. 이름: " + CurrentWeapon.name);
        }
        else
        {
            Debug.Log(weaponSlots[index - 1].WeaponData.itemName + " 슬롯은 비어있음");
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

    private Vector2 GetMousePos()
    {
        //mouse버튼 입력에 따른 위치 받아오기
        Vector2 mPos = Input.mousePosition;
        return cam.ScreenToWorldPoint(mPos);
    }
}
