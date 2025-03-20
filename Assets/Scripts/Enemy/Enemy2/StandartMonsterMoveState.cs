using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMonsterMoveState : StandartMonsterGroundedState
{
    public StandartMonsterMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartMonster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName, _enemy)
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

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState);
    }
}
