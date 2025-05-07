using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusDownState : EnemyState
{
    private Succubus enemy;
    public SuccubusDownState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.flyTimer = 0;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState);
    }
}
