using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(0, null);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(0);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);      
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

        if (xInput == 0 || player.isBusy)
            stateMachine.ChangeState(player.idleState);

        
    }
}
