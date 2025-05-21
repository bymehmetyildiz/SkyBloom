using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyDeadState : EnemyState
{
    private Ivy enemy;
    public IvyDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(42, null);
    }
}
