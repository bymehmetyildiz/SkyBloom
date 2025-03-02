using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    // States
    public BanditIdleState idleState { get; private set; }
    public BanditMoveState moveState { get; private set; }
    public BanditBattleState battleState { get; private set; }
    public BanditAttackState attackState { get; private set; }
    public BanditStunState stunState { get; private set; }
    public BanditDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new BanditIdleState(this, stateMachine, "Idle", this);
        moveState = new BanditMoveState(this, stateMachine, "Move", this);
        battleState = new BanditBattleState(this, stateMachine, "Move", this);
        attackState = new BanditAttackState(this, stateMachine, "Attack", this);
        stunState = new BanditStunState(this, stateMachine, "Stun", this);
        deadState = new BanditDeadState(this, stateMachine, "Dead", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();

        if (stats.isDamaged && (stateMachine.currentState == idleState || stateMachine.currentState == moveState))
            stateMachine.ChangeState(battleState);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }

    public override void Dead()
    {
        base.Dead();
        stateMachine.ChangeState(deadState);
    }
}
