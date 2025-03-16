using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueIdleState : EnemyState
{
    private Rogue enemy;
    public RogueIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
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
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
        if ((enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1) && stateTimer < 0)
            stateMachine.ChangeState(enemy.battleState);
    }
}
