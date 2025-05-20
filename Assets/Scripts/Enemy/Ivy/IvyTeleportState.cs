using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyTeleportState : EnemyState
{
    private Ivy enemy;
    public IvyTeleportState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.canBeDamaged = false;
        AudioManager.instance.PlaySFX(41, null);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.canBeDamaged = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.idleState);
    }
}
