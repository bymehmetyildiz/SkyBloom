using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfBattleState : EnemyState
{
    private WereWolf wereWolf;
    
    private int moveDir;

    public WereWolfBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf
        ) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();        

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


        if (wereWolf.IsPlayerDetected())
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

        if (wereWolf.IsWallDetected() || !wereWolf.IsGroundDetected())
            stateMachine.ChangeState(wereWolf.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, wereWolf.transform.position) > 10)
            stateMachine.ChangeState(wereWolf.idleState);

    }
}
