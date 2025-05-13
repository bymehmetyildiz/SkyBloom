using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succubus : Enemy
{
    //Variables
    public float riseDistance;
    public float flySpeed;   
    [HideInInspector] public Enemy currentEnemy;
    [SerializeField] private SpawnThunder thunder;
    [SerializeField] private BoxCollider2D arena;
    public float flyTimer;
    public float flyDur;
    public DialogueTrigger npc;
    public DialogueManager dialogueManager;

    //States
    public SuccubusHumanState humanState {  get; private set; }
    public SuccubusTransformState transformState { get; private set; }
    public SuccubusIdleState idleState { get; private set; }
    public SuccubusMoveState moveState { get; private set; }
    public SuccubusUpState upState { get; private set; }
    public SuccubusFlyState flyState { get; private set; }
    public SuccubusDownState downState { get; private set; }
    public SuccubusAttackState attackState { get; private set; }
    public SuccubusKickState kickState { get; private set; }
    public SuccubusStunState stunState { get; private set; }
    public SuccubusDeadState deadState { get; private set; }
    public SuccubusBattleState battleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        humanState = new SuccubusHumanState(this, stateMachine, "HumanIdle", this);
        transformState = new SuccubusTransformState(this, stateMachine, "Transform", this);
        idleState = new SuccubusIdleState(this, stateMachine, "Idle", this);
        moveState = new SuccubusMoveState(this, stateMachine, "Move", this);
        upState = new SuccubusUpState(this, stateMachine, "Up", this);
        flyState = new SuccubusFlyState(this, stateMachine, "Fly", this);
        downState = new SuccubusDownState(this, stateMachine, "Down", this);
        attackState = new SuccubusAttackState(this, stateMachine, "Attack", this);
        kickState = new SuccubusKickState(this, stateMachine, "Kick", this);
        stunState = new SuccubusStunState(this, stateMachine, "Stun", this);
        deadState = new SuccubusDeadState(this, stateMachine, "Dead", this);
        battleState = new SuccubusBattleState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(humanState);
        arena = GameManager.instance.GetComponent<BoxCollider2D>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    protected override void Update()
    {
        base.Update();
        flyTimer += Time.deltaTime;

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

    public IEnumerator SpawnThunder()
    {
        yield return new WaitForSeconds(1);
        Vector2 pos = transform.position; // Default to some safe value

        if (player.transform.position.x > arena.bounds.min.x + 3 && player.transform.position.x < arena.bounds.max.x - 3)
            pos = new Vector2(player.transform.position.x, transform.position.y) + Vector2.right * player .facingDir;
        else if (player.transform.position.x <= arena.bounds.min.x + 3)
            pos = new Vector2(arena.bounds.min.x + 3, transform.position.y);
        else if (player.transform.position.x >= arena.bounds.max.x - 3)
            pos = new Vector2(arena.bounds.max.x - 3, transform.position.y);

        SpawnThunder newThunder =  Instantiate(thunder, pos, Quaternion.identity);
    }
        

}
