using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyFlipState : EnemyState
{
    private RangedEnemy enemy;  

    public RangedEnemyFlipState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, RangedEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.CanFlip())        
            enemy.SetVelocity(5 * -enemy.facingDir, 10);        
        else        
            enemy.SetVelocity(5 * enemy.facingDir, 10);
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

        if (triggerCalled)
           stateMachine.ChangeState(enemy.attackState);


        
    }
}
