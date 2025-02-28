using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public EntityStats entityStats { get; private set; }

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackCheckDistance;

    [Header("Flip Info")]
    public int facingDir = 1;
    protected bool facingRight = true;

    [Header("Knockback Info")]
    public Vector2 knockBackPower;
    public int knockBackDir;
    protected Vector2 defaultKnockBack;
    [SerializeField] protected float knockBackDur;
    protected bool isKnocked;

    [Header("Block Info")]
    [SerializeField] private GameObject blockEffect;


    [Header("Stun Info")]
    public float stunDur;
    public Vector2 stunDir;
    protected bool canStun;
    [SerializeField] protected GameObject counterImage;

    public System.Action onFlipped;


    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        entityStats = GetComponent<EntityStats>();
        defaultKnockBack = knockBackPower;
    }


    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    public virtual void FreezeEntity(float _slowPercentage, float _slowDur)
    {

    }

    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }

   

    // Collision Check
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckDistance);
    }

    //Damage Check
    public void DamageEffect()
    {
        StartCoroutine(fx.FlashFX()); 

        StartCoroutine(KnockBack());
        
    }


    // Knockback
    public virtual void SetupKnockBackDir(Transform _damageDireciton)
    {
        if (_damageDireciton.position.x > transform.position.x)
            knockBackDir = -1;
        else if(_damageDireciton.position.x < transform.position.x)
            knockBackDir = 1;
    }

    public virtual IEnumerator KnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockBackPower.x * knockBackDir, knockBackPower.y);
        yield return new WaitForSeconds(knockBackDur);
        isKnocked = false;
        SetupDefaultKnockback();
    }

    public void SetupKnockbackPower(Vector2 _knockbackPower) => knockBackPower = _knockbackPower;

    protected virtual void SetupDefaultKnockback()
    {

    }


    // Velocity Check
    public void SetVelocity(float _xVelocity, float _yvelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yvelocity);
        FlipController(_xVelocity);
    }

    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;

        rb.velocity = Vector2.zero;
    }
    

    // Flip Check
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null)
            onFlipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    //Instantiate Block Effect
    public void SpawnBlockEffect(Transform _transform)
    {
        Instantiate(blockEffect, _transform.position, Quaternion.identity);
    }


    // Dead
    public virtual void Dead()
    {

    }

   

}
