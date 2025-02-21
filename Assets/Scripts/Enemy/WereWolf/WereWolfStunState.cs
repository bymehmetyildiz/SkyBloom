using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfStunState : EnemyState
{
    private WereWolf wereWolf;
    public WereWolfStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();

        wereWolf.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = wereWolf.stunDur;
        rb.velocity = new Vector2(-wereWolf.facingDir * wereWolf.stunDir.x, wereWolf.stunDir.y);

    }

    public override void Exit()
    {
        base.Exit();
        wereWolf.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(wereWolf.idleState);
    }
}
