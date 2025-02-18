using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolf : Enemy
{
    // States
    public WereWolfIdleState idleState { get; private set; }
    public WereWolfMoveState moveState { get; private set; }
    public WereWolfBattleState battleState { get; private set; }
    public WereWolfAttackState attackState { get; private set; }
    public WereWolfStunState stunState { get; private set; }
    public WereWolfDeadState deadState { get; private set; }
    public WereWolfHumanIdleState humanIdleState { get; private set; }
    public WereWolfTransformState transformState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new WereWolfIdleState(this, stateMachine, "Idle", this);
        moveState = new WereWolfMoveState(this, stateMachine, "Move", this);
        battleState = new WereWolfBattleState(this, stateMachine, "Move", this);
        attackState = new WereWolfAttackState(this, stateMachine, "Attack", this);
        stunState = new WereWolfStunState(this, stateMachine, "Stun", this);
        deadState = new WereWolfDeadState(this, stateMachine, "Dead", this);
        humanIdleState = new WereWolfHumanIdleState(this, stateMachine, "HumanIdle", this);
        transformState = new WereWolfTransformState(this, stateMachine, "Transform", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(humanIdleState);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();

       
    }

    public override bool CanBeStunned()
    {
        return base.CanBeStunned();
    }

    public override void Dead()
    {
        base.Dead();
    }
}
