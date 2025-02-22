using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfPrepareState : EnemyState
{
    private WereWolf wereWolf;

    public WereWolfPrepareState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();
        wereWolf.StartCounterAttack();
    }

    public override void Exit()
    {
        base.Exit();
        wereWolf.StopCounterAttack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(wereWolf.runAttackState);
    }
}
