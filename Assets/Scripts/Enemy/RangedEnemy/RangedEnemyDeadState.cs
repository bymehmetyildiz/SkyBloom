using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyDeadState : EnemyState
{
    private RangedEnemy enemy;
    public RangedEnemyDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, RangedEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
