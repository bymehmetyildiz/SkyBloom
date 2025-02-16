using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    // States
    public WolfIdleState idleState { get; private set; }
    public WolfMoveState moveState { get; private set; }
    public WolfBattleState battleState { get; private set; }
    public WolfAttackState attackState { get; private set; }
    public WolfStunState stunState { get; private set; }
    public WolfDeadState deadState { get; private set; }

   

    protected override void Awake()
    {
        base.Awake();

        idleState = new WolfIdleState(this, stateMachine, "Idle", this);
        moveState = new WolfMoveState(this, stateMachine, "Move", this);
        battleState = new WolfBattleState(this, stateMachine, "Move", this);
        attackState = new WolfAttackState(this, stateMachine, "Attack", this);
        stunState = new WolfStunState(this, stateMachine, "Stun", this);
        deadState = new WolfDeadState(this, stateMachine, "Dead", this);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
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
