using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerMoveState : MaulerGroundedState
{
    public MaulerMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName, _mauler)
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

        mauler.SetVelocity(mauler.moveSpeed * mauler.facingDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (mauler.IsWallDetected() || !mauler.IsGroundDetected())
            stateMachine.ChangeState(mauler.idleState);
    }
}
