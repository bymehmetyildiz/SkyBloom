using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueDeadState : EnemyState
{
    private Rogue enemy;
    public RogueDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(30, enemy.transform);
    }
}
