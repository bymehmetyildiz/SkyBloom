using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyTrapState : EnemyState
{
    private Ivy enemy;
    public IvyTrapState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
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

        if (triggerCalled)
            stateMachine.ChangeState(enemy.idleState);

        if (enemy.stats.isDamaged)
            stateMachine.ChangeState(enemy.hitState);
    }
}
