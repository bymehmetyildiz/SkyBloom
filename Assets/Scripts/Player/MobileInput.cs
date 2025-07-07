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
        if (player.IsGroundDetected() && !player.isBusy)
            player.stateMachine.ChangeState(player.jumpState);

        if (!player.IsGroundDetected() && player.IsWallDetected())
            player.stateMachine.ChangeState(player.wallJumpState);
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
