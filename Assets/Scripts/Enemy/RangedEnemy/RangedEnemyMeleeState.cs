using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyMeleeState : EnemyState
{
    private RangedEnemy enemy;
    public RangedEnemyMeleeState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, RangedEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        if (player.stats.isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.flipState);
    }
}
