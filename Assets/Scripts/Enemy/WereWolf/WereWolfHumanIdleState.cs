using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfHumanIdleState : WereWolfGroundedState
{
    public WereWolfHumanIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName, _wereWolf)
    {
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

        if (GameManager.instance.isPlayerEnetered || Vector2.Distance(wereWolf.transform.position, player.transform.position) < 1 || wereWolf.stats.isDamaged) 
            stateMachine.ChangeState(wereWolf.transformState);

    }
}
