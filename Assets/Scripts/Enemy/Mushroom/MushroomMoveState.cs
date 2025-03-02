using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMoveState : MushroomGroundedState
{
    public MushroomMoveState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName, _mushroom)
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

        mushroom.SetVelocity(mushroom.moveSpeed * mushroom.facingDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (mushroom.IsWallDetected() || !mushroom.IsGroundDetected())
            stateMachine.ChangeState(mushroom.idleState);
    }
}
