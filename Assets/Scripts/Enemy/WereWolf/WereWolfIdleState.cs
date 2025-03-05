using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfIdleState : WereWolfGroundedState
{
    private int blockChance;
    public WereWolfIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName, _wereWolf)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = wereWolf.idleTime;
        wereWolf.SetZeroVelocity();

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

        if (wereWolf.stats.isDead)
            return;

        if (player.stateMachine.currentState == player.primaryAttackState && blockChance > 1 && player.stunTrigger)
        {
            wereWolf.canBeDamaged = false;
            stateMachine.ChangeState(wereWolf.blockState);
            player.stateMachine.ChangeState(player.stunState);
        }


        if (stateTimer < 0.0f)
        {
            if ((wereWolf.IsPlayerDetected() || Vector2.Distance(player.transform.position, wereWolf.transform.position) < 1))
                stateMachine.ChangeState(wereWolf.battleState);

            else
            {
                wereWolf.Flip();
                stateMachine.ChangeState(wereWolf.moveState);
            }

        }
    }
}
