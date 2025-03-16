using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAmbushState : EnemyState
{
    private Rogue enemy;
    public RogueAmbushState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled && enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState);
    }
}
