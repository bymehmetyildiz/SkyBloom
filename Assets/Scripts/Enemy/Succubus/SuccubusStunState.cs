using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusStunState : EnemyState
{
    private Succubus enemy;
    public SuccubusStunState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("StunFX", 0, 0.1f);

        stateTimer = enemy.stunDur;

        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDir.x, enemy.stunDir.y);


    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.CancelColorChange();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
