using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GrimIdleState : EnemyState
{
    private Grim enemy;
    public GrimIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Grim _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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

        if (enemy.stats.isDead)
            return;

        if (stateTimer < 0.0f)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.battleState);
        }

        if (enemy.IsPlayerDetected())
        {
            if(Vector2.Distance(player.transform.position, enemy.transform.position) < enemy.attackDistance)
                stateMachine.ChangeState(enemy.attackState);

            else if (Vector2.Distance(player.transform.position, enemy.transform.position) >= enemy.attackDistance)
                stateMachine.ChangeState(enemy.battleState);

        }
    }
}
