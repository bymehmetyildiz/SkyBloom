using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : EnemyState
{
    private Monster enemy;
    public MonsterAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Monster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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

        if (triggerCalled)
        {
            if(enemy.IsPlayerDetected())
                stateMachine.ChangeState(enemy.idleState);

            else if (!enemy.IsPlayerDetected())
                stateMachine.ChangeState(enemy.attack2State);

            triggerCalled = false;
        }
    }
}
