using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlordMoveState : EnemyState
{
    private Warlord enemy;
    public WarlordMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Warlord _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (((enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1)) && !enemy.IsWallDetected())
            stateMachine.ChangeState(enemy.battleState);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected() || enemy.IsDangerDetected())
            stateMachine.ChangeState(enemy.idleState);
    }
}
