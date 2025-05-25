using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemy : Enemy
{
    // States
    public EnemyIdleState idleState { get; private set; }
    public EnemyMoveState moveState { get; private set; }
    public EnemyBattleState battleState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    public EnemyStunState stunState { get; private set; }
    public EnemyDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new EnemyIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyAttackState(this, stateMachine, "Attack", this);
        stunState = new EnemyStunState(this, stateMachine, "Stun", this);
        deadState = new EnemyDeadState(this, stateMachine, "Dead", this);
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
