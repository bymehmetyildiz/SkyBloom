using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlordBattleState : EnemyState
{
    private Warlord enemy;
    private int moveDir; 
    private float distanceToPlayer;

    public WarlordBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Warlord _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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

        enemy.SetVelocity(enemy.moveSpeed * moveDir * 1.5f, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        // Early return if not on ground
        if (!enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.moveState);
            return;
        }

        // Check player detection
        var playerHit = enemy.IsPlayerDetected();

        if (playerHit || (distanceToPlayer <= enemy.attackDistance && player.IsGroundDetected()))
        {
            stateTimer = enemy.agroTime;

            if (playerHit.distance <= enemy.attackDistance && !enemy.IsWallDetected())
            {
                stateMachine.ChangeState(enemy.attackState);
                return;
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10f)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        // Check environment hazards
        if (enemy.IsWallDetected() || enemy.IsDangerDetected())
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        
       
    }
}
