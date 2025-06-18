using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;

        player.stats.Invincible(true);

        AudioManager.instance.PlaySFX(4, null);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
        player.stats.Invincible(false);
        AudioManager.instance.StopSFX(4);
       
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocity(player.dashSpeed * player.facingDir, 0);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected() && player.IsLedgeDetected())
            stateMachine.ChangeState(player.wallSlideState);
        

        if (stateTimer < 0 || player.isBusy)
            stateMachine.ChangeState(player.idleState);
    }
}
