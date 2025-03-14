using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangedType
{
    Ranged,
    Melee,
}

public class RangedEnemy : Enemy
{
    public RangedType rangedType;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPoint;
    public bool releaseProjectile;

    // States
    public RangedEnemyIdleState idleState { get; private set; }
    public RangedEnemyMoveState moveState { get; private set; }
    public RangedEnemyBattleState battleState { get; private set; }
    public RangedEnemyAttackState attackState { get; private set; }
    public RangedEnemyStunState stunState { get; private set; }
    public RangedEnemyDeadState deadState { get; private set; }
    public RangedEnemyMeleeState meleeState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        idleState = new RangedEnemyIdleState(this, stateMachine, "Idle", this);
        moveState = new RangedEnemyMoveState(this, stateMachine, "Move", this);
        battleState = new RangedEnemyBattleState(this, stateMachine, "Move", this);
        attackState = new RangedEnemyAttackState(this, stateMachine, "Attack", this);
        stunState = new RangedEnemyStunState(this, stateMachine, "Stun", this);
        deadState = new RangedEnemyDeadState(this, stateMachine, "Dead", this);
        meleeState = new RangedEnemyMeleeState(this, stateMachine, "Melee", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
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

    public override void ReleaseProjectile()
    {
        releaseProjectile = true;
    }

    public override void InstantiateProjectile()
    {
        GameObject newObject = Instantiate(projectile, spawnPoint.position, Quaternion.identity);

        if (facingDir < 0)
        {
            newObject.GetComponent<Projectile>().xVelocity = -1 * newObject.GetComponent<Projectile>().xVelocity;
            newObject.GetComponent<Projectile>().transform.Rotate(0, 180, 0);
        }
    }
}
