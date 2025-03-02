using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomIdleState : MushroomGroundedState
{
    public MushroomIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName, _mushroom)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = mushroom.idleTime;
        mushroom.SetZeroVelocity();

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
            if ((mushroom.IsPlayerDetected() || Vector2.Distance(player.transform.position, mushroom.transform.position) < 1))
                stateMachine.ChangeState(mushroom.battleState);

            else
            {
                mushroom.Flip();
                stateMachine.ChangeState(mushroom.moveState);
            }

        }
    }
}
