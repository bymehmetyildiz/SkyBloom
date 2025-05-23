using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeadState : EnemyState
{
    private Monster enemy;
    public MonsterDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Monster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(38, player.transform);
    }
}
