using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformedMoveState : EnemyState
{
    private TransformingEnemy enemy;
    public TransformedMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TransformingEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
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

        if (enemy.IsPlayerDetected())
            stateMachine.ChangeState(enemy.battleState);
    }
}
