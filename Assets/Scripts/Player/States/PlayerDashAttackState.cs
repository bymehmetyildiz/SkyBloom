using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttackState : PlayerState
{
    public PlayerDashAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(19, null);
        player.skillManager.dashSkill.UseSkill();
        player.stats.Invincible(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
        AudioManager.instance.StopSFX(19);
        player.stats.Invincible(false);
        
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

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }
}
