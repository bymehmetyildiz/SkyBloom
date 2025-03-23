using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemyPassiveState : EnemyState
{
    private TrapEnemy enemy;
    public TrapEnemyPassiveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TrapEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.MakeTransparent(true);
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 1 || enemy.stats.isDamaged || enemy.IsPlayerDetected())
            stateMachine.ChangeState(enemy.spawnState);
    }
}
