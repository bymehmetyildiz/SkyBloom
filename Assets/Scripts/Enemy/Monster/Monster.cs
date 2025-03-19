using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Enemy
{
    // States
    public MonsterIdleState idleState { get; private set; }
    public MonsterMoveState moveState { get; private set; }
    public MonsterBattleState battleState { get; private set; }
    public MonsterAttackState attackState { get; private set; }
    public MonsterAttack2State attack2State { get; private set; }
    public MonsterDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new MonsterIdleState(this, stateMachine, "Idle", this);
        moveState = new MonsterMoveState(this, stateMachine, "Move", this);
        battleState = new MonsterBattleState(this, stateMachine, "Battle", this);
        attackState = new MonsterAttackState(this, stateMachine, "Attack", this);
        attack2State = new MonsterAttack2State(this, stateMachine, "Attack2", this);
        deadState = new MonsterDeadState(this, stateMachine, "Dead", this);
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

    public override void Dead()
    {
        base.Dead();
        stateMachine.ChangeState(deadState);
    }
}
