using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mauler : Enemy
{
    [Header("Mauler Vars")]
    public CircleCollider2D cc;
    public bool isExpanding;
    public float spTimer;
    public float spDuration;

    //States
    public MaulerAttackState attackState { get; private set; }
    public MaulerBattleState battleState { get; private set; }
    public MaulerBlockState blockState { get; private set; }
    public MaulerDeadState deadState { get; private set; }
    public MaulerStunState stunState { get; private set; }
    public MaulerIdleState idleState { get; private set; }
    public MaulerMoveState moveState { get; private set; }
    public MaulerSpAttackState spAttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        attackState = new MaulerAttackState(this, stateMachine, "Attack", this);
        battleState = new MaulerBattleState(this, stateMachine, "Move", this);
        blockState = new MaulerBlockState(this, stateMachine, "Block", this);
        deadState = new MaulerDeadState(this, stateMachine, "Dead");
        stunState = new MaulerStunState(this, stateMachine, "Stun", this);
        idleState = new MaulerIdleState(this, stateMachine, "Idle", this);
        moveState = new MaulerMoveState(this, stateMachine, "Move", this);
        spAttackState = new MaulerSpAttackState(this, stateMachine, "SpAttack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        cc.enabled = false;
        cc.radius = 0;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override bool CanBeStunned()
    {
        stateMachine.ChangeState(stunState);
        return base.CanBeStunned();
    }

    public override void Dead()
    {
        base.Dead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            Debug.Log("Player Detected");
        }
    }
}
