using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfGroundedState : EnemyState
{
    protected WereWolf wereWolf; 

    public WereWolfGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
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

        if (wereWolf.stateMachine.currentState == wereWolf.humanIdleState || wereWolf.stateMachine.currentState == wereWolf.transformState || player.GetComponent<PlayerStats>().isDead)
            return;

        //if ((wereWolf.IsPlayerDetected() || Vector2.Distance(player.transform.position, wereWolf.transform.position) < 1))
        //    stateMachine.ChangeState(wereWolf.battleState);
        //return;

    }
}
