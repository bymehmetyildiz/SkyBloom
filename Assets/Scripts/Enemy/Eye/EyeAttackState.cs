using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAttackState : EnemyState
{
    private Eye eye;
    public EyeAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye eye) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.eye = eye;
    }

    public override void Enter()
    {
        base.Enter();
        eye.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        eye.lastAttackTime = Time.time;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (!eye.IsPlayerDetected() || eye.IsPlayerDetected().distance >= eye.agroDistance)
                stateMachine.ChangeState(eye.idleState);

            triggerCalled = false;
        }
    }
}
