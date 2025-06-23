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

    [Header("����")]
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

    #region List<string>�������� weapon�޾ƿ���
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

            musicWeapon = weapon; // �� ���� �Ҵ�
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
            // ���� ���⸦ ã�� : WeaponSpawnPos�� �ڽ����� �����ϴ� ����
            Transform slot = WeaponSpawnPos[notSet];
            musicWeapon = slot.GetComponentInChildren<MusicWeapon>(); // ���� ���� �Ҵ�


            if (musicWeapon != null)
                CheckOldWeapon();
            else
                Debug.LogWarning("���� ���⸦ ã�� �� ����.");

            return musicWeapon;
        }
    }
    #endregion
    public void CheckOldWeapon() // �Ķ���� Player
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
        for (int i = 0; i < weaponSlots.Length; i++) //������ ����ŭ Ű �Ҵ�
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) //�ڵ� Ű �Ҵ�
            {
                SelectWeapon(i); //SelectWeapon�� �ŰԺ��� �Ѱ���
                Debug.Log((i + 1) + "�� �Ǳ� ����");
                break;
            }
        }
    }

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
