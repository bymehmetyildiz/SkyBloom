using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMoveState : BanditGroundedState
{
    public BanditMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit _bandit) : base(_baseEnemy, _stateMachine, _animBoolName, _bandit)
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

        bandit.SetVelocity(bandit.moveSpeed * bandit.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (bandit.IsWallDetected() || !bandit.IsGroundDetected())
            stateMachine.ChangeState(bandit.idleState);
    }
}
