using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusTransformState : EnemyState
{
    private Succubus enemy;

    public SuccubusTransformState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.sing.Stop();
        AudioManager.instance.PlaySFX(55, null);
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
            stateMachine.ChangeState(enemy.upState);
    }
}
