using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{


    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0) && player.skillManager.aerialSlamSkill.CanUseSkill() && player.skillManager.aerialSlamSkill.IsSkillUnlocked())
            stateMachine.ChangeState(player.aerialSlamAirState);

        if (player.IsWallDetected() && player.IsLedgeDetected())        
            stateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.landState);

        if (!player.IsGroundDetected() && !player.IsLedgeDetected() && player.IsWallDetected() && !player.isHanging)
            stateMachine.ChangeState(player.ledgeGrabState);


    }
}
