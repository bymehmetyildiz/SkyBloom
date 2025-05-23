using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackState : EnemyState
{
    private Mushroom mushroom;
    public MushroomAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.mushroom = _mushroom;
    }

    public override void Enter()
    {
        base.Enter();
        mushroom.SetZeroVelocity();
        AudioManager.instance.PlaySFX(53, mushroom.transform);
    }

    public override void Exit()
    {
        base.Exit();
        mushroom.lastAttackTime = Time.time;
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
            if (!mushroom.IsPlayerDetected() || mushroom.IsPlayerDetected().distance >= mushroom.agroDistance)
                stateMachine.ChangeState(mushroom.idleState);

            triggerCalled = false;
        }
    }
}
