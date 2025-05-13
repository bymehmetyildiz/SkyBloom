using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardBattleState : EnemyState
{
    private Lizard lizard;
    private int moveDir;
    private float distanceToPlayer;

    public LizardBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        lizard = _lizard;
    }

    public override void Enter()
    {
        base.Enter();

        lizard.stats.isDamaged = false; 

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(lizard.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        distanceToPlayer = Mathf.Abs(player.transform.position.x - lizard.transform.position.x);

        if (distanceToPlayer < 0.2f)
            return;

        if (player.transform.position.x > lizard.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < lizard.transform.position.x)
            moveDir = -1;

        lizard.SetVelocity(lizard.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!lizard.IsGroundDetected())
        {
            lizard.Flip();
            stateMachine.ChangeState(lizard.moveState);
            return;
        }


        if (lizard.IsPlayerDetected() || (distanceToPlayer <= lizard.attackDistance && player.IsGroundDetected()))
        {
            stateTimer = lizard.agroTime;

            if (lizard.IsPlayerDetected().distance <= lizard.attackDistance)
                stateMachine.ChangeState(lizard.attackState);

        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, lizard.transform.position) > 10)
                stateMachine.ChangeState(lizard.idleState);
        }

        if (lizard.IsWallDetected() || !lizard.IsGroundDetected() || lizard.IsDangerDetected())
            stateMachine.ChangeState(lizard.idleState);
        
    }
}
