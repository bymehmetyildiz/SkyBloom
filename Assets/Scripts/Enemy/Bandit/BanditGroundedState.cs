using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditGroundedState : EnemyState
{
    protected Bandit bandit;

    public BanditGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit _bandit) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        bandit = _bandit;
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

        if ((bandit.IsPlayerDetected() || Vector2.Distance(player.transform.position, bandit.transform.position) < 1))
            stateMachine.ChangeState(bandit.battleState);
        //return;
    }
}
