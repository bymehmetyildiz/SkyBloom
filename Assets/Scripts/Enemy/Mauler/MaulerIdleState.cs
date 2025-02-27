using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerIdleState : MaulerGroundedState
{
    public MaulerIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName, _mauler)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = mauler.idleTime;
        mauler.SetZeroVelocity();
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
            mauler.Flip();
            stateMachine.ChangeState(mauler.moveState);
        }
    }
}
