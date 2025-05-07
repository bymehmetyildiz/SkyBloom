using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusAttackState : EnemyState
{
    private Succubus enemy;
    public SuccubusAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
