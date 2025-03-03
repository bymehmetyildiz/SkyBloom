using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeStunState : EnemyState
{
    private Eye eye;
    public EyeStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye _eye) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.eye = _eye;
    }

    public override void Enter()
    {
        base.Enter();

        eye.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = eye.stunDur;

        rb.velocity = new Vector2(-eye.facingDir * eye.stunDir.x, eye.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        eye.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(eye.idleState);
    }
}
