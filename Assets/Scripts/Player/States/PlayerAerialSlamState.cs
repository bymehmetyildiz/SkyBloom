using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAerialSlamState : PlayerState
{
    public PlayerAerialSlamState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.fx.ScreenShake();

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
