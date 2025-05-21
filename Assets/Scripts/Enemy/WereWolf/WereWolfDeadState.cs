using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolfDeadState : EnemyState
{
    private WereWolf wereWolf;
    public WereWolfDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(38, null);
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
