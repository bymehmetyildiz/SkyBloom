using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RangedEnemyAttackState : EnemyState
{
    private RangedEnemy enemy;

    public RangedEnemyAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, RangedEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();


        if (!enemy.IsPlayerDetected())
            enemy.Flip();
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

        var hit = enemy.IsPlayerDetected();

        if (enemy.releaseProjectile)
        {
            enemy.InstantiateProjectile();
            enemy.releaseProjectile = false;
        }

        if (hit && hit.distance <= enemy.attackCheckDistance)
        {
            if (enemy.rangedType == RangedType.Melee)
                stateMachine.ChangeState(enemy.meleeState);
        }

        if (triggerCalled)
        {
            if (!hit || hit.distance >= enemy.agroDistance)
                stateMachine.ChangeState(enemy.idleState);

            triggerCalled = false;
        }

    }
}
