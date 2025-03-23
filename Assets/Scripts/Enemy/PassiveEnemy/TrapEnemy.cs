using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemy : Enemy
{
    // States
    public TrapEnemyPassiveState passiveState { get; private set; }
    public TrapEnemySpawnState spawnState { get; private set; }
    public TrapEnemyIdleState idleState { get; private set; }
    public TrapEnemyMoveState moveState { get; private set; }
    public TrapEnemyBattleState battleState { get; private set; }
    public TrapEnemyAttackState attackState { get; private set; }
    public TrapEnemyStunState stunState { get; private set; }
    public TrapEnemyDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        passiveState = new TrapEnemyPassiveState(this, stateMachine, "Passive", this);
        spawnState = new TrapEnemySpawnState(this, stateMachine, "Spawn", this);
        idleState = new TrapEnemyIdleState(this, stateMachine, "Idle", this);
        moveState = new TrapEnemyMoveState(this, stateMachine, "Move", this);
        battleState = new TrapEnemyBattleState(this, stateMachine, "Move", this);
        attackState = new TrapEnemyAttackState(this, stateMachine, "Attack", this);
        stunState = new TrapEnemyStunState(this, stateMachine, "Stun", this);
        deadState = new TrapEnemyDeadState(this, stateMachine, "Dead", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(passiveState);
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
