using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusHumanState : EnemyState
{
    private Succubus enemy;
    public SuccubusHumanState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
