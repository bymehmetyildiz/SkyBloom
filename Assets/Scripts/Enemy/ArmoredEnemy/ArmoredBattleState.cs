using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredBattleState : EnemyState
{
    ArmoredEnemy enemy;
    private int moveDir;
    private float distanceToPlayer;
    public ArmoredBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, ArmoredEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
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

        if (Vector2.Distance(enemy.transform.position, player.transform.position) < 3)
        {
            if (player.stateMachine.currentState == player.primaryAttackState && player.stunTrigger)
            {
                enemy.canBeDamaged = false;
                stateMachine.ChangeState(enemy.blockState);
                player.stateMachine.ChangeState(player.stunState);
                AudioManager.instance.PlaySFX(11, null);
                AudioManager.instance.PlaySFX(9, null);
            }
        }
        


        if (!enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
            return;
        }


        if (enemy.IsPlayerDetected() || (distanceToPlayer <= enemy.attackDistance && player.IsGroundDetected()))
        {
            stateTimer = enemy.agroTime;

            if (enemy.IsPlayerDetected().distance <= enemy.attackDistance)
                stateMachine.ChangeState(enemy.attackState);

        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
                stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected() || enemy.IsDangerDetected())
            stateMachine.ChangeState(enemy.idleState);

        

    }
}
