using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAttackState : EnemyState
{
    private Rogue enemy;
    public RogueAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.teleportState);
    }
}
