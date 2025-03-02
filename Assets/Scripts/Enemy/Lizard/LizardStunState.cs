using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardStunState : EnemyState
{
    private Lizard lizard;

    public LizardStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.lizard = _lizard;
    }

    public override void Enter()
    {
        base.Enter();

        lizard.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = lizard.stunDur;

        rb.velocity = new Vector2(-lizard.facingDir * lizard.stunDir.x, lizard.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        lizard.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(lizard.idleState);
    }
}
