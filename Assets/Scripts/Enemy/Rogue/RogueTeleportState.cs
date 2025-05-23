using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueTeleportState : EnemyState
{
    private Rogue enemy;
    public RogueTeleportState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
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
            stateMachine.ChangeState(enemy.battleState);
        
    }
}
