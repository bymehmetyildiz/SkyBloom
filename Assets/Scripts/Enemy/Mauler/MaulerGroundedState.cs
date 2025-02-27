using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerGroundedState : EnemyState
{
    protected Mauler mauler;

    public MaulerGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
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
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if ((mauler.IsPlayerDetected() || Vector2.Distance(player.transform.position, mauler.transform.position) < 1))
            stateMachine.ChangeState(mauler.battleState);
    }
}
