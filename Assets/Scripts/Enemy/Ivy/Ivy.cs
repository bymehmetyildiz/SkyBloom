using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ivy : Enemy
{
    [Header("Teleport Details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundCheck;

    [Header("Branch Attack Details")]
    [SerializeField] private GameObject branch;

    [Header("Ranged Attack Details")]
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform spawnPoint;

    //States
    public IvyIdleState idleState { get; private set; }
    public IvyAttackState attackState { get; private set; }    
    public IvyTeleportState teleportState { get; private set;}
    public IvyDeadState deadState { get; private set; }
    public IvyBranchAttackState branchAttackState { get; private set; }
    public IvyTrapState trapState { get; private set;}
    public IvyHitState hitState { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        idleState = new IvyIdleState(this, stateMachine, "Idle", this);
        attackState = new IvyAttackState(this, stateMachine, "Attack", this);        
        teleportState = new IvyTeleportState(this, stateMachine, "Teleport", this);
        deadState = new IvyDeadState(this, stateMachine, "Dead", this);
        branchAttackState = new IvyBranchAttackState(this, stateMachine, "Branch", this);
        trapState = new IvyTrapState(this, stateMachine, "Ranged", this);
        hitState = new IvyHitState(this, stateMachine, "Hit", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        arena = GameManager.instance.GetComponent<BoxCollider2D>();
    }

    public override void Dead()
    {
        base.Dead();
        stateMachine.ChangeState(deadState);
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

    // Create Branch
    public void CreateBranch()
    {
        if(player.IsGroundDetected())
            Instantiate(branch, (Vector2)player.transform.position + Vector2.up * 0.2f, Quaternion.identity);
    }

    //Create 

    public void CreateBall()
    {
        GameObject ballPrefab = Instantiate(ball, spawnPoint.position, Quaternion.identity);
    }
}


