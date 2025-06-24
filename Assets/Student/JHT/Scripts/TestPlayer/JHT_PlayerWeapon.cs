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

    [Header("����")]
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

    #region List<string>�������� weapon�޾ƿ���
    public MusicWeapon AddMusicWeapon(MusicWeapon _musicWeapon)
    {
        if (_musicWeapon == null) return null;
        int weaponNum = 0;

        if (!WeaponList.Contains(_musicWeapon.WeaponData.itemName))
        {
            //������ ���Ⱑ WeaponList�� ��������� WeaponList<string>�� �־���
            WeaponList.Add(_musicWeapon.WeaponData.itemName);
            weaponNum = WeaponList.Count - 1;

            MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[weaponNum]);

            weaponSlots[weaponIdx] = weapon;
            weaponIdx++;
            return musicWeapon;
        }
        else
        {

            //������ ���Ⱑ WeaponList�� �־������ count,level�� ���� ���� ��ƼŬ ����
            for (int i = 0; i < WeaponList.Count; i++)
            {
                if (WeaponList[i] == _musicWeapon.WeaponData.itemName)
                {
                    weaponNum = i;
                    break;
                }
            }

            Transform slot = WeaponSpawnPos[weaponNum];
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

    //UPdate�� �ڲٵ��ư�
    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //�ڵ� Ű �Ҵ�
        {
            SelectWeapon(1); //SelectWeapon�� �ŰԺ��� �Ѱ���
            Debug.Log(1 + "�� �Ǳ� ����");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //�ڵ� Ű �Ҵ�
        {
            SelectWeapon(2); //SelectWeapon�� �ŰԺ��� �Ѱ���
            Debug.Log(2 + "�� �Ǳ� ����");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //�ڵ� Ű �Ҵ�
        {
            SelectWeapon(3); //SelectWeapon�� �ŰԺ��� �Ѱ���
            Debug.Log(3 + "�� �Ǳ� ����");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //�ڵ� Ű �Ҵ�
        {
            SelectWeapon(4); //SelectWeapon�� �ŰԺ��� �Ѱ���
            Debug.Log(4 + "�� �Ǳ� ����");
        }
    }

    private void SelectWeapon(int index)
    {
        if (weaponSlots[index - 1] != null)
        {
            CurrentWeapon = weaponSlots[index - 1];  //������ ���� = currentWeapon
            Debug.Log(weaponSlots[index - 1].WeaponData.itemName + "�� ���� �����. �̸�: " + CurrentWeapon.name);
        }
        else
        {
            Debug.Log(weaponSlots[index - 1].WeaponData.itemName + " ������ �������");
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
        //mouse��ư �Է¿� ���� ��ġ �޾ƿ���
        Vector2 mPos = Input.mousePosition;
        return cam.ScreenToWorldPoint(mPos);
    }
}
