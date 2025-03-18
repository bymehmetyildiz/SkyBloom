using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformedDeadState : EnemyState
{
    private TransformingEnemy enemy;
    public TransformedDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TransformingEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
