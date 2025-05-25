using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    // States
    public MushroomIdleState idleState { get; private set; }
    public MushroomMoveState moveState { get; private set; }
    public MushroomBattleState battleState { get; private set; }
    public MushroomAttackState attackState { get; private set; }
    public MushroomStunState stunState { get; private set; }
    public MushroomDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new MushroomIdleState(this, stateMachine, "Idle", this);
        moveState = new MushroomMoveState(this, stateMachine, "Move", this);
        battleState = new MushroomBattleState(this, stateMachine, "Move", this);
        attackState = new MushroomAttackState(this, stateMachine, "Attack", this);
        stunState = new MushroomStunState(this, stateMachine, "Stun", this);
        deadState = new MushroomDeadState(this, stateMachine, "Dead", this);
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

        if (stateMachine.currentState == idleState || stateMachine.currentState == moveState)
            if (stats.isDamaged)
            {
                if (!IsDangerDetected())
                    Flip();
                stateMachine.ChangeState(battleState);
            }

    }
}
