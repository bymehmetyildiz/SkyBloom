using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Animations;
using UnityEngine;

public class Player : Entity
{
    [HideInInspector]
    public PlayerStateMachine stateMachine { get; private set; }
    public SkillManager skillManager { get; private set; }
    public GameObject sword { get; private set; }
    public PlayerStats stats { get; private set; }
    public CircleCollider2D cc { get; private set; }

    [Header("Move Info")]
    public float moveSpeed = 12.0f;
    public float jumpForce = 12.0f;
    public bool landTrigger;
    public bool isHanging;
    public Vector2 climbOffset;
    private float defaultMoveSpeed;
    private float defaultJumpForce;   

    [Header("Block Info")]
    public float parryDur;

    [Header("Attack Info")]
    public bool isBusy;
    public float attackMove = 1.0f;
    public int comboCounter;
    public float catchImpact;
    public bool stunTrigger; // Variable for player to enter stun state
    

    [Header("Collision Info")]    
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private float ledgeCheckDistance;
    

    [Header("DashInfo")]
    public float dashSpeed;
    public float dashDuration;
    private float cooldownTimer; 
    [SerializeField] private float cooldown;
    public float dashDirection { get; private set; }
    private float defaultDashSpeed;

    //Move States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }   
    public PlayerLandState landState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerLedgeGrabState ledgeGrabState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    

    // Battle States
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerBlockState blockState { get; private set; }
    public PlayerStunState stunState { get; private set; }
   

    //SkillStates
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerRainCastState rainCastState { get; private set; }
    public PlayeThunderCastState thunderCastState { get; private set; }
    public PlayerDashAttackState dashAttackState { get; private set; }
    public PlayerDiscCastState discCastState { get; private set; }
    public PlayerTwisterState twisterState { get; private set; }
    public PlayerFlurrySlashState flurrySlashState { get; private set; }
    public PlayerAerialSlamAirState aerialSlamAirState { get; private set; }
    public PlayerAerialSlamState aerialSlamState { get; private set; }
    public PlayerEarthSlamState earthSlamState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        landState = new PlayerLandState(this, stateMachine, "Land");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        ledgeGrabState = new PlayerLedgeGrabState(this, stateMachine, "LedgeGrab");
        ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, "LedgeClimb");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");

        

        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        blockState = new PlayerBlockState(this, stateMachine, "Block");
        stunState = new PlayerStunState(this, stateMachine, "Stun");
       

        aimSwordState = new PlayerAimSwordState(this, stateMachine, "Aim");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "Catch");
        rainCastState = new PlayerRainCastState(this, stateMachine, "Rain");
        thunderCastState = new PlayeThunderCastState(this, stateMachine, "Thunder");
        dashAttackState = new PlayerDashAttackState(this, stateMachine, "DashAttack");
        discCastState = new PlayerDiscCastState(this, stateMachine, "DiscCast");
        twisterState = new PlayerTwisterState(this, stateMachine, "Twister");
        flurrySlashState = new PlayerFlurrySlashState(this, stateMachine, "Flurry");
        aerialSlamAirState = new PlayerAerialSlamAirState(this, stateMachine, "AerialJump");
        aerialSlamState = new PlayerAerialSlamState(this, stateMachine, "AerialSlam");
        earthSlamState = new PlayerEarthSlamState(this, stateMachine, "EarthSlam");
     }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        landTrigger = false;
        skillManager = SkillManager.instance;
        stats = GetComponent<PlayerStats>();
        cc = GetComponentInChildren<CircleCollider2D>();
        cc.enabled = false;
        cc.radius = 0;
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;        
    }


    protected override void Update()
    {
        if (Time.timeScale == 0.0f)
            return;

        base.Update();
        cooldownTimer -= Time.deltaTime;
        stateMachine.currentState.Update();
        CheckDashInput();
        CheckHealInput();
        
    }
   

    private void CheckHealInput()
    {
        if(stats.isDead) 
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && !isBusy)
            Inventory.instance.UseHealFlask();

        if (Input.GetKeyDown(KeyCode.Alpha2) && !isBusy)
            Inventory.instance.UseMagicFlask();
    }

    protected override void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    // Busy Check
    public IEnumerator CheckBusy(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }


    // Dash Check
    public void CheckDashInput()
    {
        if (stats.isDead || isBusy)
            return;

        if (IsWallDetected())
            return;

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.C) && CanUseSkill())
            {
                dashDirection = Input.GetAxisRaw("Horizontal");

                if (dashDirection == 0)
                    dashDirection = facingDir;

                stateMachine.ChangeState(dashState);
            }
        }

        //if (skillManager.dashSkill.dashPierceUnlock == false)
        //    return;

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.C) && SkillManager.instance.dashSkill.CanUseSkill() && skillManager.dashSkill.IsSkillUnlocked())
            {
                dashDirection = Input.GetAxisRaw("Horizontal");

                if (dashDirection == 0)
                    dashDirection = facingDir;

                stateMachine.ChangeState(dashAttackState);
            }
        }
    }
    private bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }

    //Sword Throw
    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;  
    }

    public void CatchSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }



    // Collision Check
    public virtual bool IsLedgeDetected() => Physics2D.Raycast(ledgeCheck.position, Vector2.right * facingDir, ledgeCheckDistance, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + ledgeCheckDistance, ledgeCheck.position.y));
    }

    // Grab check
    public IEnumerator HangCheck()
    {
        yield return new WaitForSeconds(0.5f);
        isHanging = false;
    }


    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    public override void Dead()
    {
        base.Dead();
        stateMachine.ChangeState(deadState);
    }

    // Freeze
    public override void FreezeEntity(float _slowPercentage, float _slowDur)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDur);

    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }


    // TwisterDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            CollisionDamage(collision);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("DangerZone"))
        {
            if (stats.isDead)
                return;

            stats.TakeDamage(stats.maxHealth.GetValue());          
        }
    }

    public void CollisionDamage(Collision2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();        
        entityStats.DoDamage(enemy);    
    }

    //Knockback
    protected override void SetupDefaultKnockback()
    {
        knockBackPower = new Vector2(3,3);
    }

    public bool IsGroundDetected(out float groundAngle)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        if (hit.collider != null)
        {
            // Calculate the angle of the ground surface
            Vector2 surfaceNormal = hit.normal;
            groundAngle = Mathf.Atan2(surfaceNormal.x, surfaceNormal.y) * Mathf.Rad2Deg;
            return true;
        }

        groundAngle = 0f;
        return false;
    }

    
}
