using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMoveState : WolfGroundedState
{
    public WolfMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName, _wolf)
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

        wolf.SetVelocity(wolf.moveSpeed * wolf.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (wolf.IsWallDetected() || !wolf.IsGroundDetected())
            stateMachine.ChangeState(wolf.idleState);
    }
}
