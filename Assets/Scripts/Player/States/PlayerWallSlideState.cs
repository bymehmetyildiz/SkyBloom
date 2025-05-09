using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (player.stats.isDamaged)
        {
            rb.velocity = new Vector2(5 * -player.facingDir, rb.velocity.y);
            return;
        }

            

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (yInput >= 0)
            rb.velocity = new Vector2(0, rb.velocity.y * 0.5f);
    }

    public override void Update()
    {
        base.Update();

        if ((xInput != 0 && player.facingDir != xInput && !player.IsGroundDetected()) || player.stats.isDamaged)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }

        
        if (!player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0 && player.facingDir == xInput && !player.IsGroundDetected())
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;

        }
            
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
