using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : EnemyState
{
    private TransformingEnemy enemy;
    public TransformState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TransformingEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
