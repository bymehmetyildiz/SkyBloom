using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditIdleState : BanditGroundedState
{
    public BanditIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit _bandit) : base(_baseEnemy, _stateMachine, _animBoolName, _bandit)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = bandit.idleTime;
        bandit.SetZeroVelocity();
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

        if (stateTimer < 0.0f)
        {
            bandit.Flip();
            stateMachine.ChangeState(bandit.moveState);
        }

    }
}
