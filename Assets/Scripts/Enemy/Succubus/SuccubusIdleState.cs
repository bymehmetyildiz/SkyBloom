using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusIdleState : EnemyState
{
    private Succubus enemy;
    public SuccubusIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        enemy.SetZeroVelocity();
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
