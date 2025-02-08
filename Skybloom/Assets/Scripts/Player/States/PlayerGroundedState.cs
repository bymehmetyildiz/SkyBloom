using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && !Input.GetKey(KeyCode.LeftShift))
            stateMachine.ChangeState(player.aimSwordState);

        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.blockState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttackState);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.F) && player.skillManager.rainSwordSkill.CanUseSkill())
            stateMachine.ChangeState(player.rainCastState);

        if (Input.GetKeyDown(KeyCode.E) && !player.skillManager.thunderSkill.IsBlackHoleSpawned() && player.skillManager.thunderSkill.CanUseSkill())
            stateMachine.ChangeState(player.thunderCastState);

        if (Input.GetKeyDown(KeyCode.R) && SkillManager.instance.fireDiscSkill.CanUseSkill())
            stateMachine.ChangeState(player.discCastState);

        if (Input.GetKeyDown(KeyCode.Mouse2) && player.skillManager.twisterSkill.CanUseSkill())
            stateMachine.ChangeState(player.twisterState);

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse0) && player.skillManager.flurrySlashSkill.CanUseSkill())
            stateMachine.ChangeState(player.flurrySlashState);

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Mouse1) && player.skillManager.earthSlamSkill.CanUseSkill())
            stateMachine.ChangeState(player.earthSlamState);

    }

    private bool HasNoSword()
    {
        if (!player.sword)
            return true;

        player.sword.GetComponent<SwordSkillController>().ReturnSword();
      
        
        return false;
    }

}
