using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RogueBattleState : EnemyState
{
    private Rogue enemy;
    private int moveDir;
    public RogueBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.agroTime;
            if (enemy.IsPlayerDetected().distance <= enemy.agroDistance)
                stateMachine.ChangeState(enemy.dashState);
        }
        else if (!enemy.IsPlayerDetected())
        {
            enemy.Flip();
            if (enemy.IsPlayerDetected())
            {
                stateTimer = enemy.agroTime;
                if (enemy.IsPlayerDetected().distance <= enemy.agroDistance)
                    stateMachine.ChangeState(enemy.dashState);
            }
            else
            {
                stateMachine.ChangeState(enemy.ambushState);
            }
        }

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
            stateMachine.ChangeState(enemy.idleState);

    }
}
