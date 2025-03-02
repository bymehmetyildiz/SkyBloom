using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomStunState : EnemyState
{
    private Mushroom mushroom;

    public MushroomStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.mushroom = _mushroom;
    }

    public override void Enter()
    {
        base.Enter();

        mushroom.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = mushroom.stunDur;

        rb.velocity = new Vector2(-mushroom.facingDir * mushroom.stunDir.x, mushroom.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        mushroom.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(mushroom.idleState);
    }
}
