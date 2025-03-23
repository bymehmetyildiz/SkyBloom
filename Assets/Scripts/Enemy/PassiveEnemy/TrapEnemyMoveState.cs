using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemyMoveState : TrapEnemyGroundedState
{
    public TrapEnemyMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TrapEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected() || enemy.IsDangerDetected())
            stateMachine.ChangeState(enemy.idleState);
    }
}
