using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimSpellState : EnemyState
{
    private Grim enemy;
    public GrimSpellState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Grim _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
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

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
