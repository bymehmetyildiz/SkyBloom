using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusKickState : EnemyState
{
    private Succubus enemy;
    public SuccubusKickState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(56, null);
    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled)
            stateMachine.ChangeState(enemy.idleState);
    }
}
