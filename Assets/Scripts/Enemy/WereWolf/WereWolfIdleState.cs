using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfIdleState : WereWolfGroundedState
{
    public WereWolfIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName, _wereWolf)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = wereWolf.idleTime;
        wereWolf.SetZeroVelocity();
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
            wereWolf.Flip();
            stateMachine.ChangeState(wereWolf.moveState);
        }

    }
}
