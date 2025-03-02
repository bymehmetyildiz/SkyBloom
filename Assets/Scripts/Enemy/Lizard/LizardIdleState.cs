using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardIdleState : LizardGroundedState
{


    public LizardIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName, _lizard)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = lizard.idleTime;
        lizard.SetZeroVelocity();
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
            lizard.Flip();
            stateMachine.ChangeState(lizard.moveState);
        }
    }
}
