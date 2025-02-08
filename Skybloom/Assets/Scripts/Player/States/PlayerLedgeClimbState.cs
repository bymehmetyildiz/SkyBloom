using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 stopPos;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        stopPos.Set(player.transform.position.x + ( player.climbOffset.x * player.facingDir), player.transform.position.y + player.climbOffset.y);
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 3.5f;
        player.transform.position = stopPos;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
