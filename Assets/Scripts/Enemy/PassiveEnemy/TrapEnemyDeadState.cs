using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemyDeadState : EnemyState
{
    private TrapEnemy enemy;
    public TrapEnemyDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TrapEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
}
