using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRangedState : EnemyState
{
    private Eye eye;
    public EyeRangedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye eye) : base(_baseEnemy, _stateMachine, _animBoolName)
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
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (eye.IsPlayerDetected().distance <= eye.attackDistance)
        {
            stateMachine.ChangeState(eye.attackState);
            return;
        }

        if (eye.releaseProjectile)
        {
            eye.InstantiateProjectile();
            eye.releaseProjectile = false;
        }

       

        if (triggerCalled)
            stateMachine.ChangeState(eye.battleState);
    }
}
