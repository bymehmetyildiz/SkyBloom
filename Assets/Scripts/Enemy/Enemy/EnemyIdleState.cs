using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyGroundedState
{
    public EnemyIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        enemy.SetZeroVelocity();
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

        if (stateTimer < 0.0f)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }

    }
}
