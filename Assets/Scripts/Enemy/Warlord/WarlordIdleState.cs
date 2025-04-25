using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlordIdleState : EnemyState
{
    private Warlord enemy;
    public WarlordIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Warlord _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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

        if (((enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1)) && !enemy.IsWallDetected())
            stateMachine.ChangeState(enemy.battleState);

        if (stateTimer < 0.0f)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
       
    }
}
