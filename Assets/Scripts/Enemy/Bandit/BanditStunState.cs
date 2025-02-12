using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditStunState : EnemyState
{
    Bandit bandit;

    public BanditStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit _bandit) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.bandit = _bandit;
    }

    public override void Enter()
    {
        base.Enter();

        bandit.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = bandit.stunDur;

        rb.velocity = new Vector2(-bandit.facingDir * bandit.stunDir.x, bandit.stunDir.y);

        
    }

    public override void Exit()
    {
        base.Exit();
        bandit.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(bandit.idleState);
    }
}
