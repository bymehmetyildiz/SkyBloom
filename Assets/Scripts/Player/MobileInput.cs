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
        if(PlayerManager.instance.player.isBusy == false && player.IsGroundDetected())
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
    private void Update()
    {
        if (Input.touchCount == 1 && aimButton != null)
        {
            Touch touch = Input.GetTouch(0);

            // Check if touch is within allowed aim zone
            if (aimButton.IsTouchWithinArea(touch.position))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initialTouchPosition = touch.position;
                    isAim = true;

                    if (CanStartAiming())
                        player.stateMachine.ChangeState(player.aimSwordState);
                }
                else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isAim)
                {
                    currentTouchPosition = touch.position;
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isAim)
                {
                    currentTouchPosition = touch.position;
                    Vector2 worldStart = Camera.main.ScreenToWorldPoint(initialTouchPosition);
                    Vector2 worldEnd = Camera.main.ScreenToWorldPoint(currentTouchPosition);
                    dragDirection = (worldStart - worldEnd).normalized;

                    if (player.skillManager.swordSkill.swordType != SwordType.None && HasNoSword())
                        player.skillManager.swordSkill.CreateSword();

                    isAim = false;
                }
            }
        }
        else
        {
            isAim = false;
        }
    }

    private bool CanStartAiming()
    {
        return HasNoSword() &&
               player.skillManager.swordSkill.swordType != SwordType.None &&
               player.stats.currentMagic >= player.skillManager.swordSkill.magicAmount &&
               !player.isBusy;
    }

    public void MobileAim(bool isAim)
    {
        if (this.isAim == isAim) return; // Prevent re-entry
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
    // End Aiming

    // Use Potions
    public void UseHealthPotion()
    {
        if (player.stats.currentHealth < player.stats.GetMaxHealth() && !player.isBusy)        
            Inventory.instance.UseHealFlask();
    }

    public void UseMagicPotion()
    {
        if (player.stats.currentMagic < player.stats.maxMagic.GetValue() && !player.isBusy)        
            Inventory.instance.UseMagicFlask();
    }
}
