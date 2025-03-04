using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyGroundedState
{
    public EnemyMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName, _enemy)
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

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState);
    }
}
