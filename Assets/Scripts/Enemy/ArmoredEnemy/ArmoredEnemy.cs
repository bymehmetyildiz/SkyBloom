using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : Enemy
{
    // States
    public ArmoredIdleState idleState { get; private set; }
    public ArmoredMoveState moveState { get; private set; }
    public ArmoredBattleState battleState { get; private set; }
    public ArmoredAttackState attackState { get; private set; }
    public ArmoredStunState stunState { get; private set; }
    public ArmoredDeadState deadState { get; private set; }
    public ArmoredBlockState blockState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        idleState = new ArmoredIdleState(this, stateMachine, "Idle", this);
        moveState = new ArmoredMoveState(this, stateMachine, "Move", this);
        battleState = new ArmoredBattleState(this, stateMachine, "Move", this);
        attackState = new ArmoredAttackState(this, stateMachine, "Attack", this);
        stunState = new ArmoredStunState(this, stateMachine, "Stun", this);
        deadState = new ArmoredDeadState(this, stateMachine, "Dead", this);        
        blockState = new ArmoredBlockState(this, stateMachine, "Block", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();

        if (stateMachine.currentState == idleState || stateMachine.currentState == moveState)
            if (stats.isDamaged)
            {
                if (!IsDangerDetected())
                    Flip();
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
