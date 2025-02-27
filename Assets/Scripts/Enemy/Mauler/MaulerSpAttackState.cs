using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerSpAttackState : EnemyState
{
    private Mauler mauler;

    public MaulerSpAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mauler = _mauler;
    }

    public override void Enter()
    {
        base.Enter();
        mauler.cc.enabled = true;
        mauler.isExpanding = true;

    }

    public override void Exit()
    {
        base.Exit();
        mauler.cc.enabled = false;
        mauler.cc.radius = 0;
        mauler.isExpanding = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (mauler.isExpanding)
        {
            mauler.cc.radius = Mathf.MoveTowards(player.cc.radius, 0.0f, 0.1f * Time.deltaTime);
        }

        if (triggerCalled)
            stateMachine.ChangeState(mauler.idleState);
    }
}
