using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    public Transform[] WeaponSpawnPos;

    [Header("무기")]
    [SerializeField] private MusicWeapon defaultWeapon;
    public MusicWeapon[] weaponSlots;
    public MusicWeapon currentWeapon;

    Coroutine attackDelayCor;
    private bool canAttack = true;
    private float delay = 0;
    private float maxCount => currentWeapon.curAttackDelay;
    private float defaultMaxCount => defaultWeapon.curAttackDelay;

    Coroutine showStatusCor;
    Property<bool> isShowStatus = new();
    private void Start()
    {       
        defaultWeapon = GetComponentInChildren<MusicWeapon>();
        defaultWeapon.Init(transform);
        weaponSlots = Manager.Data.PlayerStatus.PlayerWeapons;       
    }

    private void OnEnable()
    {
        Manager.Data.PlayerStatus.OnChangedWeapon += AddMusicWeapon;
        Manager.Data.PlayerStatus.OnCurrentWeaponChanged += WeaponSwitch;
        Manager.Input.GetPlayerBind("Attack").AddStartedEvent(Attack);
        Manager.Input.GetPlayerBind("Attack").AddCanceledEvent(CancelAttack);
        Manager.Input.GetPlayerBind("Status").AddStartedEvent(ShowStatus);
        Manager.Input.GetPlayerBind("Status").AddCanceledEvent(CloseStatus);
        isShowStatus.AddEvent(ShowStatusView);
    }

    private void OnDisable()
    {
        Manager.Data.PlayerStatus.OnChangedWeapon -= AddMusicWeapon;
        Manager.Data.PlayerStatus.OnCurrentWeaponChanged -= WeaponSwitch;
        Manager.Input.GetPlayerBind("Attack").RemoveStartedEvent(Attack);
        Manager.Input.GetPlayerBind("Attack").RemoveCanceledEvent(CancelAttack);
        Manager.Input.GetPlayerBind("Status").RemoveStartedEvent(ShowStatus);
        Manager.Input.GetPlayerBind("Status").RemoveCanceledEvent(CloseStatus);
        isShowStatus.RemoveEvent(ShowStatusView);
    }

    private void Update()
    {
        #region 버튼 연속으로 누름방지
        //코루틴에서 셜정이 잘 안돼서 누를때 제외 연속으로 버튼 누를시 계속나오는거 방지
        if (currentWeapon != null)
        {
            if (delay < maxCount)
            {
                delay += Time.deltaTime;
                canAttack = false;
            }
            else
            {
                canAttack = true; ;
            }
        }
        else
        {
            if (delay < defaultMaxCount)
            {
                delay += Time.deltaTime;
                canAttack = false;
            }
            else
            {
                canAttack = true; ;
            }
        }
        #endregion
    }

    private void ShowStatus(InputAction.CallbackContext ctx)
    {
        if (!Manager.Game.IsDead && gameObject != null)
            isShowStatus.Value = true;
    }

    private void CloseStatus(InputAction.CallbackContext ctx)
    {
        if(isShowStatus.Value && gameObject != null)
            isShowStatus.Value = false;
    }

    //V키 누를 시 플레이어 스탯창
    private void ShowStatusView(bool value)
    {
        if(value)
        {
            //Manager.UI.StatusView.gameObject.SetActive(true);
        }
        else
        {
            //Manager.UI.StatusView.gameObject.SetActive(false);
        }
    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        if (!canAttack) return;

        if (currentWeapon != null)
        {
            Manager.Game.IsPress.Value = true;
            SetProjectile(currentWeapon);
        }
        else
        {
            Manager.Game.IsPress.Value = true;
            SetProjectile(defaultWeapon);
        }

        
    }

    private void CancelAttack(InputAction.CallbackContext ctx)
    {
        if(currentWeapon != null)
        {
            Manager.Game.IsPress.Value = false;
        }

        if (attackDelayCor != null)
        {
            StopCoroutine(attackDelayCor);
            attackDelayCor = null;
            delay = 0;
        }
    } 

    #region List<string>형식으로 weapon받아오기
    public void AddMusicWeapon(int idx, MusicWeapon _musicWeapon)
    {
        Debug.Log("Invoke Event");
        if (_musicWeapon == null) return;
        Debug.Log("Not Return Event");     
        MusicWeapon weapon = Instantiate(_musicWeapon, WeaponSpawnPos[idx]);
        weapon.Init(transform);
        weaponSlots[idx] = weapon;
    }
    #endregion

    private void WeaponSwitch(int _idx)
    {
        if (currentWeapon != null && currentWeapon.WeaponData.ID == 5)
        {
            Manager.Game.IsPress.Value = false; // 강제로 레이저 끄기
        }
        currentWeapon = weaponSlots[_idx];
    }


    private void SelectWeapon(int index)
    {
        if (weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];  //선택한 무기 = currentWeapon
            Debug.Log((index + 1) + "번 무기 사용중.selectWeapon 이름: " + currentWeapon.name);
            
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

        if (Manager.Game.IsPause || Manager.Game.IsDead)
            return;

        if (attackDelayCor != null)
            return;

        if (musicWeapon.WeaponData.Type == MusicWeaponType.Violin)
        {
            attackDelayCor = StartCoroutine(ViolinAttack(musicWeapon));
        }
        else
        {
            attackDelayCor = StartCoroutine(AttackCor(musicWeapon));
        }
    }

    #region 일반무기 공격속도
    IEnumerator AttackCor(MusicWeapon musicWeapon)
    {
        while (Manager.Game.IsPress.Value)
        {
            Manager.Game.OnPlayerAttack?.Invoke();
            musicWeapon.Attack(GetMousePos());

            yield return AttackDelay(musicWeapon);
        }
        yield return AttackDelay(musicWeapon);
        attackDelayCor = null;
        yield return null;
    }
    #endregion

    #region 바이올린 공격속도
    IEnumerator ViolinAttack(MusicWeapon musicWeapon)
    {
        Manager.Game.OnPlayerAttack?.Invoke();
        musicWeapon.Attack(GetMousePos());

        yield return Utile.GetDelay(1 / musicWeapon.curAttackDelay * Manager.Data.PlayerStatus.AttackSpeed.Value);
        
        attackDelayCor = null;
        yield return null;
    }
    #endregion

    private Vector2 GetMousePos() => Manager.Input.GetMousePosition();


    #region 무기 공속
    private WaitForSeconds AttackDelay(MusicWeapon musicWeapon)
    {
        if (musicWeapon.WeaponData.ID == 1)
        {
            return Utile.GetDelay(1 / musicWeapon.WeaponData.AttackDelay[0] *
                Manager.Data.PlayerStatus.AttackSpeed.Value);
        }
        else
        {
            return Utile.GetDelay(1 / musicWeapon.curAttackDelay * Manager.Data.PlayerStatus.AttackSpeed.Value);
        }
    }
#endregion

}
