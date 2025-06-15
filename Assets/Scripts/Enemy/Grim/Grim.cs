using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grim : Enemy
{
    [SerializeField] private GameObject spell;
    [SerializeField] private float spellOffset;

    // States
    public GrimIdleState idleState { get; private set; }
    public GrimBattleState battleState { get; private set; }
    public GrimAttackState attackState { get; private set; }
    public GrimSpellState spellState { get; private set; }
    public GrimDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new GrimIdleState(this, stateMachine, "Idle", this);
        battleState = new GrimBattleState(this, stateMachine, "Move", this);
        attackState = new GrimAttackState(this, stateMachine, "Attack", this);
        spellState = new GrimSpellState(this, stateMachine, "Spell", this);
        deadState = new GrimDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if (stateMachine.currentState == idleState || stateMachine.currentState == battleState)
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

    public void CreateSpell()
    {
        Instantiate(spell, (Vector2)player.transform.position + Vector2.up * spellOffset , Quaternion.identity);
    }

}
