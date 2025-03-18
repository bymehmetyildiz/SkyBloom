using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformedBattleState : EnemyState
{
    private TransformingEnemy enemy;
    private int moveDir;

    public TransformedBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, TransformingEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.stats.isDamaged = false;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
            return;
        }

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.agroTime;

            if (enemy.IsPlayerDetected().distance <= enemy.attackDistance)
                stateMachine.ChangeState(enemy.attackState);

        }

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10 || enemy.IsWallDetected())
            stateMachine.ChangeState(enemy.idleState);

    }
}
