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

        if (enemy.releaseProjectile)
        {
            enemy.InstantiateProjectile();
            enemy.releaseProjectile = false;
        }

        if (triggerCalled)
        {
            if (!enemy.IsPlayerDetected() || enemy.IsPlayerDetected().distance >= enemy.agroDistance)
                stateMachine.ChangeState(enemy.idleState);

            else if (enemy.IsPlayerDetected().distance <= enemy.attackCheckDistance)
            {
                if(enemy.rangedType == RangedType.Melee)
                    stateMachine.ChangeState(enemy.meleeState);
            }

            triggerCalled = false;
        }
        
        
    }
}
