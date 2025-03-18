using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformingMoveState : EnemyState
{
    private TransformingEnemy enemy;
    public TransformingMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TransformingEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
            stateMachine.ChangeState(enemy.tidleState);

        if (enemy.IsPlayerDetected())
            stateMachine.ChangeState(enemy.transformState);
    }
}
