using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : EnemyState
{
    private Monster enemy;
    public MonsterIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Monster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
            if ((enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1))
                stateMachine.ChangeState(enemy.battleState);
            else
            {
                enemy.Flip();
                stateMachine.ChangeState(enemy.moveState);
            }
            
        }

       
    }
}
