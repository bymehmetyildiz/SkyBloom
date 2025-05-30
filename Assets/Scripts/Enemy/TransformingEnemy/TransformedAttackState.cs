using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformedAttackState : EnemyState
{
    private TransformingEnemy enemy;
    public TransformedAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TransformingEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled || player.stats.isDead)
        {
            if (!enemy.IsPlayerDetected() || enemy.IsPlayerDetected().distance >= enemy.agroDistance)
                stateMachine.ChangeState(enemy.idleState);

            triggerCalled = false;
        }
    }
}
