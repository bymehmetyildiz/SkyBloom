using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyBattleState : EnemyState
{
    private Ivy enemy;
    private float moveDir;

    public IvyBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.stats.isDamaged = false;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.idleState);
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
            stateMachine.ChangeState(enemy.idleState);
            return;
        }


        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.agroTime;

            if (enemy.IsPlayerDetected().distance <= enemy.attackDistance)
                stateMachine.ChangeState(enemy.attackState);

        }

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
            stateMachine.ChangeState(enemy.idleState);

    }
}
