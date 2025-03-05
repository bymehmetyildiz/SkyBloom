using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredBlockState : EnemyState
{
    private ArmoredEnemy enemy;
    private float blocDur = 1.0f;
    public ArmoredBlockState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, ArmoredEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();        
        stateTimer = blocDur;
        enemy.SetZeroVelocity();
        enemy.fx.ScreenShake();
        enemy.SpawnBlockEffect(enemy.attackCheck);
        enemy.shiledIcon.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.canBeDamaged = true;
        enemy.shiledIcon.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.battleState);           
        
    }
}
