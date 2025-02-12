using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float agroTime;
    private float defaultMoveSpeed;
    

    [Header("Stun Info")]
    public float stunDur;
    public Vector2 stunDir;
    protected bool canStun;
    [SerializeField] protected GameObject counterImage;

    [Header("Collision Info")]
    [SerializeField] protected LayerMask player;
    [SerializeField] private float detectDistance;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector] public float lastAttackTime;

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        defaultMoveSpeed = moveSpeed;
    }

    
    protected override void Update()
    {
        stateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    //Freeze Time
    public virtual void FreezeTime(bool _isFrozen)
    {
        if (_isFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }

    public virtual IEnumerator FreezeTimeFor(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }



    // Stun
    public virtual void StartCounterAttack()
    {
        canStun = true;
        counterImage.SetActive(true);
    }

    public virtual void StopCounterAttack()
    {
        canStun = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canStun)
        {
            StopCounterAttack();
            return true;
        }
        return false;

    }


    //Checks
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, detectDistance, player);

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + detectDistance, wallCheck.position.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));

        //Gizmos.DrawWireCube(wallCheck.position, new Vector3(wallCheck.position.x + distance, wallCheck.position.y));
    }

    // Freeze Effect
    public override void FreezeEntity(float _slowPercentage, float _slowDur)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDur);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        
    }

    // Violent Knockback
    protected override IEnumerator KnockBack(int _direction)
    {
        if (PlayerManager.instance.player.stateMachine.currentState == PlayerManager.instance.player.twisterState ||
            PlayerManager.instance.player.stateMachine.currentState == PlayerManager.instance.player.aerialSlamState)
            StartCoroutine(ViolentKnockBack());
        

        return base.KnockBack(_direction);
    }
    
    public IEnumerator ViolentKnockBack()
    {
        knockBackDir = new Vector2(10, 10);
        yield return new WaitForSeconds(0.1f);
        knockBackDir = defaultKnockBack;
    }
}
