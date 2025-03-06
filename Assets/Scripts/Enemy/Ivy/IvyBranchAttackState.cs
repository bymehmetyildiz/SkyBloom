using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyBranchAttackState : EnemyState
{
    private Ivy enemy;
    public IvyBranchAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
