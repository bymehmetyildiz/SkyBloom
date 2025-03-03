using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
    public bool releaseProjectile;
    [SerializeField] private GameObject projectile;

    //States
    public EyeIdleState idleState { get; private set; }
    public EyeMoveState moveState { get; private set; }
    public EyeBattleState battleState { get; private set; }
    public EyeAttackState attackState { get; private set; }
    public EyeRangedState  rangedState { get; private set; }
    public EyeStunState stunState { get; private set; }
    public EyeDeadState deadState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        idleState = new EyeIdleState(this, stateMachine, "Idle", this);
        moveState = new EyeMoveState(this, stateMachine, "Idle", this);
        battleState = new EyeBattleState(this, stateMachine, "Idle", this);
        attackState = new EyeAttackState(this, stateMachine, "Attack", this);
        stunState = new EyeStunState(this, stateMachine, "Stun", this);
        deadState = new EyeDeadState(this, stateMachine, "Dead");
        rangedState = new EyeRangedState(this, stateMachine, "Ranged", this);

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

    public override void ReleaseProjectile()
    {
        releaseProjectile = true;
    }

    public override void InstantiateProjectile()
    {
        Instantiate(projectile, attackCheck.position, Quaternion.identity);
    }
}
