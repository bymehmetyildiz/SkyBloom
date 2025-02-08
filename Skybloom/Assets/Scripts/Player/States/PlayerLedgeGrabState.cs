using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeGrabState : PlayerState
{
 
    public PlayerLedgeGrabState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.0f;
        player.isHanging = true;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 3.5f;
        player.StartCoroutine(player.HangCheck());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void Update()
    {
        base.Update();

        if (yInput < 0 && player.isHanging)        
            stateMachine.ChangeState(player.airState);  

        if (Input.GetKeyDown(KeyCode.Space) && player.isHanging)
            stateMachine.ChangeState(player.wallJumpState);

        if ((yInput > 0 || xInput == player.facingDir) && player.isHanging)
            stateMachine.ChangeState(player.ledgeClimbState);
    }
}
