using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredAttackState : EnemyState
{
    ArmoredEnemy enemy;
    public ArmoredAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, ArmoredEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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
        AudioManager.instance.StopSFX(5);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (!enemy.IsPlayerDetected() || enemy.IsPlayerDetected().distance >= enemy.agroDistance)
                stateMachine.ChangeState(enemy.idleState);

            triggerCalled = false;
        }
    }
}
