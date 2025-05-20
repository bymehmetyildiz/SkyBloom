using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimDeadState : EnemyState
{
    private Grim enemy;
    public GrimDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Grim _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(34, enemy.transform);
    }
}
