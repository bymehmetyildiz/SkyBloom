using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WereWolfBattleState : EnemyState
{
    private WereWolf wereWolf;    
    private int moveDir;
    private float distanceToPlayer;

    public WereWolfBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf
        ) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();

        wereWolf.stats.isDamaged = false;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(wereWolf.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        distanceToPlayer = Mathf.Abs(player.transform.position.x - wereWolf.transform.position.x);

        if (distanceToPlayer < 0.2f)
            return;

        if (player.transform.position.x > wereWolf.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < wereWolf.transform.position.x)
            moveDir = -1;

        wereWolf.SetVelocity(wereWolf.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!wereWolf.IsGroundDetected())
        {
            wereWolf.Flip();
            stateMachine.ChangeState(wereWolf.moveState);
            return;
        }


        if (wereWolf.IsPlayerDetected() || (distanceToPlayer <= wereWolf.attackDistance && player.IsGroundDetected()))
        {
            
            stateTimer = wereWolf.agroTime;

            if (wereWolf.IsPlayerDetected().distance <= wereWolf.attackDistance)
            {
                //if(CanAttack())
                stateMachine.ChangeState(wereWolf.attackState);
                //else
                //    stateMachine.ChangeState(bandit.idleState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, wereWolf.transform.position) > 10)
                stateMachine.ChangeState(wereWolf.idleState);
        }

        if (wereWolf.IsWallDetected() || !wereWolf.IsGroundDetected() || wereWolf.IsDangerDetected())
            stateMachine.ChangeState(wereWolf.idleState);

        

    }
}
