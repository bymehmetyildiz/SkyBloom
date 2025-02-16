using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStunState : EnemyState
{
    private Wolf wolf;

    public WolfStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wolf = _wolf;
    }

    public override void Enter()
    {
        base.Enter();

        wolf.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = wolf.stunDur;

        rb.velocity = new Vector2(-wolf.facingDir * wolf.stunDir.x, wolf.stunDir.y);


    }

    public override void Exit()
    {
        base.Exit();
        wolf.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(wolf.idleState);
    }
}
