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
    

    }

    public override void Exit()
    {
        base.Exit();
        mauler.canExpand = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();


        if (triggerCalled)
            stateMachine.ChangeState(mauler.idleState);
    }
}
