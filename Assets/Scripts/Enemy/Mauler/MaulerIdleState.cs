using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerIdleState : MaulerGroundedState
{
    private int blockChance;
    public MaulerIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName, _mauler)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = mauler.idleTime;
        mauler.SetZeroVelocity();

        blockChance = Random.Range(0, 5);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if(mauler.stats.isDead)
            return;

        if (player.stateMachine.currentState == player.primaryAttackState && blockChance > 1 && player.stunTrigger)
        {
            stateMachine.ChangeState(mauler.blockState);
            player.stateMachine.ChangeState(player.stunState);
        }


        if (stateTimer < 0.0f)
        {
            if ((mauler.IsPlayerDetected() || Vector2.Distance(player.transform.position, mauler.transform.position) < 1))
                stateMachine.ChangeState(mauler.battleState);

            else
            {
                mauler.Flip();
                stateMachine.ChangeState(mauler.moveState);
            }

        }
    }
}
