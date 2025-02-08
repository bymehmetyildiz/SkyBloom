using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAerialSlamAirState : PlayerState
{
    public PlayerAerialSlamAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.aerialSlamState);

    }
}
