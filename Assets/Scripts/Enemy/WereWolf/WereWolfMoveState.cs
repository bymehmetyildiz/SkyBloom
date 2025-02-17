using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfMoveState : WereWolfGroundedState
{
    public WereWolfMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName, _wereWolf)
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

        wereWolf.SetVelocity(wereWolf.moveSpeed * wereWolf.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (wereWolf.IsWallDetected() || !wereWolf.IsGroundDetected())
            stateMachine.ChangeState(wereWolf.idleState);
    }
}
