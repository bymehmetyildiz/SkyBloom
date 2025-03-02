using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardMoveState : LizardGroundedState
{
    public LizardMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName, _lizard)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        lizard.SetVelocity(lizard.moveSpeed * lizard.facingDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (lizard.IsWallDetected() || !lizard.IsGroundDetected())
            stateMachine.ChangeState(lizard.idleState);
    }
}
