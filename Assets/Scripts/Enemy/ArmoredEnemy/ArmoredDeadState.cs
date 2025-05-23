using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredDeadState : EnemyState
{
    ArmoredEnemy enemy;
    public ArmoredDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, ArmoredEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(25, enemy.transform);
    }
}
