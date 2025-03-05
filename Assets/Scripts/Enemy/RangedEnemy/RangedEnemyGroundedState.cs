using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RangedEnemyGroundedState : EnemyState
{
    protected RangedEnemy enemy;
    public RangedEnemyGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, RangedEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();

        if ((enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1))
            stateMachine.ChangeState(enemy.battleState);
    }
}
