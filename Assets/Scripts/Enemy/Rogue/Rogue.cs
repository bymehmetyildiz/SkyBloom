using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Enemy
{
    [Header("Teleport Details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundCheck;

    //States
    public RogueIdleState idleState { get; private set; }
    public RogueMoveState moveState { get; private set; }
    public RogueAttackState attackState { get; private set; }
    public RogueStunState stunState { get; private set; }
    public RogueBattleState battleState { get; private set; }
    public RogueDeadState deadState { get; private set; }
    public RogueDashState dashState { get; private set; }
    public RogueTeleportState teleportState { get; private set; }
    public RogueAmbushState ambushState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new RogueIdleState(this, stateMachine, "Idle", this);
        moveState = new RogueMoveState(this, stateMachine, "Move", this);
        attackState = new RogueAttackState(this, stateMachine, "Attack", this);
        stunState = new RogueStunState(this, stateMachine, "Stun", this);
        battleState = new RogueBattleState(this, stateMachine, "Move", this);
        deadState = new RogueDeadState(this, stateMachine, "Dead", this);
        dashState = new RogueDashState(this, stateMachine, "Dash", this);
        teleportState = new RogueTeleportState(this, stateMachine, "Teleport", this);
        ambushState = new RogueAmbushState(this, stateMachine, "Ambush", this);

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

    // Teleport
    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (bc.size.y / 2));

        if (!GroundBelow() || IsSomethingAround())
            FindPosition();
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool IsSomethingAround() => Physics2D.BoxCast(transform.position, surroundCheck, 0, Vector2.zero, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundCheck);

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
