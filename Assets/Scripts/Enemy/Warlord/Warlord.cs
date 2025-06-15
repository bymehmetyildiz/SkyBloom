using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlord : Enemy
{
    // States
    public WarlordIdleState idleState { get; private set; }
    public WarlordMoveState moveState { get; private set; }
    public WarlordBattleState battleState { get; private set; }
    public WarlordAttackState attackState { get; private set; }
    public WarlordWaitState waitState { get; private set; }
    public WarlordDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new WarlordIdleState(this, stateMachine, "Idle", this);
        moveState = new WarlordMoveState(this, stateMachine, "Move", this);
        battleState = new WarlordBattleState(this, stateMachine, "Move", this);
        attackState = new WarlordAttackState(this, stateMachine, "Attack", this);
        waitState = new WarlordWaitState(this, stateMachine, "Idle", this);
        deadState = new WarlordDeadState(this, stateMachine, "Dead", this);
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
                if (!IsPlayerDetected())
                    Flip();
                stateMachine.ChangeState(battleState);
            }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
  
    public override void Dead()
    {
        base.Dead();
        stateMachine.ChangeState(deadState);
    }
}
