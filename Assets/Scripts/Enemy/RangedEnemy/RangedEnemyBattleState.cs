using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBattleState : EnemyState
{
    private RangedEnemy enemy;
    private int moveDir;
    private float distanceToPlayer;

    public RangedEnemyBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, RangedEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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

        distanceToPlayer = Mathf.Abs(player.transform.position.x - enemy.transform.position.x);

        if (distanceToPlayer < 0.2f)
            return;

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


        if (enemy.IsPlayerDetected() || (distanceToPlayer <= enemy.attackDistance && player.IsGroundDetected()))
        {
            stateTimer = enemy.agroTime;

            if (enemy.IsPlayerDetected().distance <= enemy.agroDistance)
                stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
                stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected() )
            stateMachine.ChangeState(enemy.idleState);

    }
}
