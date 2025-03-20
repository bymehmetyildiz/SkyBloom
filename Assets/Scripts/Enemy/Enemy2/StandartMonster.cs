using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMonster : Enemy
{
    //States
    public StandartMonsterIdleState idleState { get; private set; }
    public StandartMonsterMoveState moveState { get; private set; }
    public StandartMonsterBattleState battleState { get; private set; }
    public StandartMonsterAttackState attackState { get; private set; }
    public StandartMonsterDeadState deadState { get; private set; }
    public StandartMonsterStunState stunState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new StandartMonsterIdleState(this, stateMachine, "Idle", this);
        moveState = new StandartMonsterMoveState(this, stateMachine, "Move", this);
        battleState = new StandartMonsterBattleState(this, stateMachine, "Battle", this);
        attackState = new StandartMonsterAttackState(this, stateMachine, "Attack", this);
        stunState = new StandartMonsterStunState(this, stateMachine, "Stun", this);
        deadState = new StandartMonsterDeadState(this, stateMachine, "Dead", this);
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
