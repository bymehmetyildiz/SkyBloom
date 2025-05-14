using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrimBattleState : EnemyState
{
    private Grim enemy;
    private int moveDir;
    private float rangedTimer;
    private float distanceToPlayer;

    public GrimBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Grim _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        rangedTimer = 1;

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

        rangedTimer -= Time.deltaTime;

        if (enemy.IsPlayerDetected() || (distanceToPlayer <= enemy.attackDistance/2 && player.IsGroundDetected()))
        {
            stateTimer = enemy.agroTime;

            if (enemy.IsPlayerDetected().distance <= enemy.attackDistance)
                stateMachine.ChangeState(enemy.attackState);
        }

        else if (!enemy.IsPlayerDetected())                    
            stateMachine.ChangeState(enemy.idleState);

        if (rangedTimer < 0)
            stateMachine.ChangeState(enemy.spellState);

        if (!enemy.IsGroundDetected() || enemy.IsWallDetected() || enemy.IsDangerDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }

    }
}
