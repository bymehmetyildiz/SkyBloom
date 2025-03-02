using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardAttackState : EnemyState
{
    private Lizard lizard;

    public LizardAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Lizard _lizard) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.lizard = _lizard;
    }

    public override void Enter()
    {
        base.Enter();
        lizard.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        lizard.lastAttackTime = Time.time;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (Vector2.Distance(player.transform.position, lizard.transform.position) > lizard.attackDistance)
                stateMachine.ChangeState(lizard.battleState);
        }
    }
}
