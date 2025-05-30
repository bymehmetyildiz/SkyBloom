using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{


    public PlayerLandState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(3, null);
    }

    public override void Exit()
    {
        base.Exit();
        player.landTrigger = false;
        AudioManager.instance.StopSFX(3);


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.landTrigger)
            stateMachine.ChangeState(player.idleState);
    }
}
