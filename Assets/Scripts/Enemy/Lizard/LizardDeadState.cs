using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardDeadState : EnemyState
{
    private Lizard lizard;

    public LizardDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.lizard = _lizard;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(48, lizard.transform);
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
