using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemyGroundedState : EnemyState
{
    protected TrapEnemy enemy;
    public TrapEnemyGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TrapEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();

        if ((enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1))
            stateMachine.ChangeState(enemy.battleState);
        //return;
    }
}
