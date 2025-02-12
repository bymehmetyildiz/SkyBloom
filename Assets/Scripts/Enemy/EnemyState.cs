using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy baseEnemy;

    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string animBoolName;

    protected float stateTimer;


    public EnemyState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.baseEnemy = _baseEnemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = baseEnemy.rb;
        baseEnemy.anim.SetBool(animBoolName, true);
        
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {
        baseEnemy.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationTrigger()
    {
        triggerCalled = true;
    }



}
