using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeIdleState : EyeGroundedState
{
    public EyeIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye _eye) : base(_baseEnemy, _stateMachine, _animBoolName, _eye)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = eye.idleTime;
        eye.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0.0f)
        {
            if ((eye.IsPlayerDetected() || Vector2.Distance(player.transform.position, eye.transform.position) < 1))
                stateMachine.ChangeState(eye.battleState);

            else
            {
                eye.Flip();
                stateMachine.ChangeState(eye.moveState);
            }

        }
    }
}
