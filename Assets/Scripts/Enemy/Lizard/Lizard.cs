using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Enemy
{
    // States
    public LizardIdleState idleState { get; private set; }
    public LizardMoveState moveState { get; private set; }
    public LizardBattleState battleState { get; private set; }
    public LizardAttackState attackState { get; private set; }
    public LizardStunState stunState { get; private set; }
    public LizardDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new LizardIdleState(this, stateMachine, "Idle", this);
        moveState = new LizardMoveState(this, stateMachine, "Move", this);
        battleState = new LizardBattleState(this, stateMachine, "Move", this);
        attackState = new LizardAttackState(this, stateMachine, "Attack", this);
        stunState = new LizardStunState(this, stateMachine, "Stun", this);
        deadState = new LizardDeadState(this, stateMachine, "Dead", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
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

    protected override void Update()
    {
        base.Update();

        if (stats.isDamaged && (stateMachine.currentState == idleState || stateMachine.currentState == moveState))
            stateMachine.ChangeState(battleState);
        
    }
}
