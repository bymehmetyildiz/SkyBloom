using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlordDeadState : EnemyState
{
    private Warlord enemy;
    public WarlordDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Warlord _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
