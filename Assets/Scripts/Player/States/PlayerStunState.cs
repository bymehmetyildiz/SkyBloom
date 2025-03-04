using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(-player.facingDir * player.stunDir.x, player.stunDir.y);
        player.GetComponentInChildren<AnimationTrigger>().AnimTrigger();
        triggerCalled = false; // Bir yukarýda ki method yüzünden true oluyor. Tekrar false yapýyorum :)
        player.stunTrigger = false;
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

        if (triggerCalled )
            stateMachine.ChangeState(player.idleState);
    }
}
