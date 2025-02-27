using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerStunState : EnemyState
{
    private Mauler mauler;

    public MaulerStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mauler = _mauler;
    }

    public override void Enter()
    {
        base.Enter();

        mauler.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = mauler.stunDur;
        rb.velocity = new Vector2(-mauler.facingDir * mauler.stunDir.x, mauler.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        mauler.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(mauler.idleState);
    }
}
