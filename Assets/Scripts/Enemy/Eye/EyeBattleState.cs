using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBattleState : EnemyState
{
    private Eye eye;
    private int moveDir;
    private float rangedTimer;
    public EyeBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye _eye) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        eye = _eye;
    }

    public override void Enter()
    {
        base.Enter();

        eye.stats.isDamaged = false;
        rangedTimer = 1;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(eye.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > eye.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < eye.transform.position.x)
            moveDir = -1;

        eye.SetVelocity(eye.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        rangedTimer -= Time.deltaTime;

        if (!eye.IsGroundDetected())
        {
            eye.Flip();
            stateMachine.ChangeState(eye.moveState);
            return;
        }

        if (rangedTimer < 0)
            stateMachine.ChangeState(eye.rangedState);

        if (eye.IsPlayerDetected())
        {
            stateTimer = eye.agroTime;

            if (eye.IsPlayerDetected().distance <= eye.attackDistance)
            {
                stateMachine.ChangeState(eye.attackState);
            }

        }

        if (eye.IsWallDetected() || !eye.IsGroundDetected() || eye.IsDangerDetected())
            stateMachine.ChangeState(eye.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, eye.transform.position) > 10)
            stateMachine.ChangeState(eye.idleState);
    }
}
