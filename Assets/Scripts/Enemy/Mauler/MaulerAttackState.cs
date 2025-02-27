using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerAttackState : EnemyState
{
    Mauler mauler;

    public MaulerAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mauler = _mauler;
    }

    public override void Enter()
    {
        base.Enter();
        mauler.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        mauler.StopCounterAttack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (player.gameObject.GetComponent<PlayerStats>().isDead && triggerCalled)
            stateMachine.ChangeState(mauler.idleState);


        if (triggerCalled)
        {
            if (!mauler.IsPlayerDetected() || mauler.IsPlayerDetected().distance >= mauler.agroDistance)
                stateMachine.ChangeState(mauler.idleState);

            triggerCalled = false;
        }
    }
}
