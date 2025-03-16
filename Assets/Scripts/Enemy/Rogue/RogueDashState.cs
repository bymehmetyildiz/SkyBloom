using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueDashState : EnemyState
{
    private Rogue enemy;
    public RogueDashState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.1f;
        rb.velocity = new Vector2(enemy.facingDir * enemy.stunDir.x * 2, enemy.stunDir.y * 4);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
       
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsGroundDetected() && stateTimer < 0)
            stateMachine.ChangeState(enemy.attackState);
    }
}
