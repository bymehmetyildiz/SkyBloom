using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMoveState : EyeGroundedState
{
    public EyeMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye _eye) : base(_baseEnemy, _stateMachine, _animBoolName, _eye)
    {

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        eye.SetVelocity(eye.moveSpeed * eye.facingDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (eye.IsWallDetected() || !eye.IsGroundDetected() || eye.IsDangerDetected())
            stateMachine.ChangeState(eye.idleState);
    }
}
