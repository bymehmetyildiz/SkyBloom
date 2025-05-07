using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusDeadState : EnemyState
{
    private bool isBossDead;
    private Succubus enemy;
    public SuccubusDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        isBossDead = false;
    }

    public override void Exit()
    {
        base.Exit();
        isBossDead = true;
    }
}
