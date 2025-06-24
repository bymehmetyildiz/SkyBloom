using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance;

    public float xInput;
    public float yInput;
    public bool isPointerOver;
    public bool isJumped;
    public bool isDashed;
    public bool isDashPierced;
    public bool useMobileInputInEditor = false;
    public bool isInventoryOpen = false;
    public bool isGamePaused = false;
    private Player player;
    public bool isAiming;
    public Vector2 aimScreenPosition;

    // Aiming
    [HideInInspector]
    public Vector2 initialTouchPosition;
    public Vector2 currentTouchPosition;
    public Vector2 dragDirection;
    public bool isAim;
    public AimButton aimButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isJumped = false;
        isDashed = false;
        isDashPierced = false;
        player = PlayerManager.instance.player;
       
    }

    //Movement
    public void OnMoveButtonDown(float _xInput) => xInput = _xInput;
    public void OnMoveButtonUp() => xInput = 0f;
    ///////


    //Jump
    public void MobileJumpState()
    {
        if(!isJumped)
        {
            isJumped = true;
            yInput = 1f;
        }
    }
    ///////
    

    //Dash
    public void MobileDashStateDown()
    {
        if (!isDashed)
        {
            isDashed = true; 
        }

    }

    public void MobileDashStateUp()
    {
        if (isDashed)
        {
            isDashed = false;
        }
    }
    ///////
   
    //Block
    public void MobileBlockStateDown()
    {
        if(PlayerManager.instance.player.isBusy == false)
            PlayerManager.instance.player.stateMachine.ChangeState(PlayerManager.instance.player.blockState);
    }

    //End Block

    // Attack
    public void MobileAttackStateDown()
    {
        if (player.IsGroundDetected())
        {
            if (PlayerManager.instance.player.isBusy == false)
                PlayerManager.instance.player.stateMachine.ChangeState(PlayerManager.instance.player.primaryAttackState);
        }
        else
        {
            if (player.stateMachine.currentState == player.airState)
            {
                if (player.skillManager.aerialSlamSkill.CanUseSkill() && player.skillManager.aerialSlamSkill.IsSkillUnlocked())
                    player.stateMachine.ChangeState(player.aerialSlamAirState);
            }
        }
    }
    //End Attack

    //Inventory
    public void OpenInventory()
    {
        if (isInventoryOpen == false)
        {
            isInventoryOpen = true;
        }
    }
    //End Inventory

    // Pause
    public void Pause()
    {
        if (isGamePaused == false)
        {
            isGamePaused = true;
        }
    }
    //End Pause

    //Skills
    public void RainCastSkill()
    {
        if (player.skillManager.swordRainSkill.CanUseSkill() && player.skillManager.swordRainSkill.IsSkillUnlocked() && !player.isBusy)
            player.stateMachine.ChangeState(player.rainCastState);
    }

    public void ThunderCastSkill()
    {
        if (!player.skillManager.thunderSkill.IsBlackHoleSpawned() &&
            player.skillManager.thunderSkill.CanUseSkill() &&
            player.skillManager.thunderSkill.IsSkillUnlocked() && !player.isBusy)
            player.stateMachine.ChangeState(player.thunderCastState);
    }

    public void DiscCastSkill()
    {
        if (player.skillManager.fireDiscSkill.CanUseSkill() && player.skillManager.fireDiscSkill.IsSkillUnlocked() && !player.isBusy)
            player.stateMachine.ChangeState(player.discCastState);
    }

    public void TwisterSkill()
    {
        if (player.skillManager.twisterSkill.CanUseSkill() && player.skillManager.twisterSkill.IsSkillUnlocked() && !player.isBusy)
            player.stateMachine.ChangeState(player.twisterState);
    }

    public void FlurrySlashSkill()
    {
        if (player.skillManager.flurrySlashSkill.CanUseSkill() && player.skillManager.flurrySlashSkill.IsSkillUnlocked() && !player.isBusy)
            player.stateMachine.ChangeState(player.flurrySlashState);
    }

    public void EarthSlamState()
    {
        if (player.skillManager.earthSlamSkill.CanUseSkill() && player.skillManager.earthSlamSkill.IsSkillUnlocked() && !player.isBusy)
            player.stateMachine.ChangeState(player.earthSlamState);
    }

    public void DashPierceSkill()
    {
        if (!isDashPierced)
        {
            isDashPierced = true;
        }

    }

    public void ChangeSwordSkill(bool _up)
    {
        if (_up)        
            player.skillManager.swordSkill.NextSwordType();        
        else
            player.skillManager.swordSkill.PreviousSwordType();
    }

    //End Skills

    // Aiming
    void Update()
    {
        if (Input.touchCount > 0 && isAim)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                initialTouchPosition = touch.position;
                currentTouchPosition = touch.position;
                dragDirection = Vector2.zero;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                currentTouchPosition = touch.position;
                dragDirection = (currentTouchPosition - initialTouchPosition).normalized;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Convert screen positions to world positions
                Vector2 worldInitial = Camera.main.ScreenToWorldPoint(initialTouchPosition);
                Vector2 worldCurrent = Camera.main.ScreenToWorldPoint(currentTouchPosition);

                // Direction: from player to the drag vector (slingshot style: pull back and release)
                Vector2 dragDir = (worldInitial - worldCurrent).normalized;

                // Store this direction for SwordSkill to use
                dragDirection = dragDir;
            }
        }
        else if (!isAim)
        {
            dragDirection = Vector2.zero;
        }
    }

    public void MobileAim(bool isAim)
    {
        this.isAim = isAim;

        if (HasNoSword() 
            && player.skillManager.swordSkill.swordType != SwordType.None 
            && player.stats.currentMagic >= player.skillManager.swordSkill.magicAmount 
            && !player.isBusy && isAim)
                player.stateMachine.ChangeState(player.aimSwordState);
    }

    private bool HasNoSword()
    {
        if (!player.sword)
            return true;

        player.sword.GetComponent<SwordSkillController>().ReturnSword();


        return false;
    }


}
