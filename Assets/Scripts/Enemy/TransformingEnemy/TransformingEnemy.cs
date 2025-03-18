using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformingEnemy : Enemy
{
    //States
    public TransformingIdleState tidleState { get; private set; }
    public TransformingMoveState tmoveState { get; private set; }
    public TransformState transformState { get; private set; }
    public TransformedIdleState idleState { get; private set; }
    public TransformedMoveState moveState { get; private set; }
    public TransformedAttackState attackState { get; private set; }
    public TransformedBattleState battleState { get; private set; }
    public TransformedStunState stunState { get; private set; }
    public TransformedDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        tidleState = new TransformingIdleState(this, stateMachine, "Idle", this);
        tmoveState = new TransformingMoveState(this, stateMachine, "Move", this);
        transformState = new TransformState(this, stateMachine, "Transform", this);
        idleState = new TransformedIdleState(this, stateMachine, "TransformedIdle", this);
        moveState = new TransformedMoveState(this, stateMachine, "TransformedMove", this);
        attackState = new TransformedAttackState(this, stateMachine, "Attack", this);
        battleState = new TransformedBattleState(this, stateMachine, "TransformedMove", this);
        stunState = new TransformedStunState(this, stateMachine, "Stun", this);
        deadState = new TransformedDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(tidleState);
    }
    protected override void Update()
    {
        base.Update();

        if (stats.isDamaged)
        {
            if (stateMachine.currentState == tidleState || stateMachine.currentState == tmoveState)
                stateMachine.ChangeState(transformState);
            else if (stateMachine.currentState == idleState || stateMachine.currentState == moveState)
                stateMachine.ChangeState(battleState);
        }
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
