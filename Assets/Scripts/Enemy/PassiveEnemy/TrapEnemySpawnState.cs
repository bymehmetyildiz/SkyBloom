using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemySpawnState : EnemyState
{
    private TrapEnemy enemy;
    public TrapEnemySpawnState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TrapEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(49, null);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.idleState);
    }
}
