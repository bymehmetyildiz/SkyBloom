using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardGroundedState : EnemyState
{
    protected Lizard lizard;

    public LizardGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.lizard = _lizard;
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

        if ((lizard.IsPlayerDetected() || Vector2.Distance(player.transform.position, lizard.transform.position) < 1))
            stateMachine.ChangeState(lizard.battleState);    
        

    }
}
