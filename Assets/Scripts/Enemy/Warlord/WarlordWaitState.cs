using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlordWaitState : EnemyState
{
    private Warlord enemy;
    public WarlordWaitState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Warlord _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1.0f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0.0f)
        {
            if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.agroDistance)
                stateMachine.ChangeState(enemy.attackState);
            else
                stateMachine.ChangeState(enemy.battleState);
        }

    }
}
