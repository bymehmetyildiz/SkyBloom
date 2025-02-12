using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditDeadState : EnemyState
{
    private Bandit bandit;
    public BanditDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit bandit) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.bandit = bandit;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
       
    }
}
