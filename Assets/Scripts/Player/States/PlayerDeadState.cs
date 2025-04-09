using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.GetComponentInChildren<AnimationTrigger>().AnimTrigger();

        UI_Controller.instance.SwitchOnEndScreen();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(player.IsGroundDetected())
            player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();
      
    }
}
