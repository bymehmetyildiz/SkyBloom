using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mauler : Enemy
{
    [SerializeField] private CircleCollider2D cc;
    [SerializeField] private float expandSpeed;
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
        cc = GetComponentInChildren<CircleCollider2D>();
        DeactivateSpAttackCollider();

    }

    protected override void Update()
    {
        base.Update();

        if (stats.isDamaged && (stateMachine.currentState == idleState || stateMachine.currentState == moveState))
            stateMachine.ChangeState(battleState);

        ExpandSpecialAttack();
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

    // Special Attack
    public void ExpandSpecialAttack()
    {
        if (canExpand)
        {
            cc.enabled = true;  
            cc.radius = Mathf.MoveTowards(cc.radius, 1, expandSpeed);
        }
        else
            DeactivateSpAttackCollider();
    }

    private void DeactivateSpAttackCollider()
    {
        cc.enabled = false;
        cc.radius = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            player.SetupKnockbackPower(new Vector2(7, 10));
            entityStats.DoMagicalDamage(playerStats);
        }
    }
}
