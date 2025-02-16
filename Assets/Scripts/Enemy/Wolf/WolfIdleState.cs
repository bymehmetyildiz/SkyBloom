using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdleState : WolfGroundedState
{
    public WolfIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName, _wolf)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = wolf.idleTime;
        wolf.SetZeroVelocity();
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
            wolf.Flip();
            stateMachine.ChangeState(wolf.moveState);
        }

    }
}
