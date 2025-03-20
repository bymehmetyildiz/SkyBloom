using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMonsterDeadState : EnemyState
{
    private StandartMonster enemy;
    public StandartMonsterDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartMonster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
