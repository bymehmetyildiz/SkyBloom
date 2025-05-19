using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMonsterAttackState : EnemyState
{
    private StandartMonster enemy;
    public StandartMonsterAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartMonster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        AudioManager.instance.PlaySFX(31, player.transform);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
        AudioManager.instance.StopSFX(31);
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
