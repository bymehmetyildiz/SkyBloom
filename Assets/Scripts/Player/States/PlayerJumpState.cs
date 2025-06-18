using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(5, null);
        player.rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();  
        AudioManager.instance.StopSFX(5);
        MobileInput.Instance.isJumped = false; // Reset jump state for mobile input
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}
