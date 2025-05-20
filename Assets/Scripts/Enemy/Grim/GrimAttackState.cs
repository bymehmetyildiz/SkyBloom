using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimAttackState : EnemyState
{
    private Grim enemy;
    public GrimAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Grim _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        AudioManager.instance.PlaySFX(33, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
        AudioManager.instance.StopSFX(33);
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
