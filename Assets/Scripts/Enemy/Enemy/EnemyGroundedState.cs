using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyGroundedState : EnemyState
{
    protected StandartEnemy enemy;

    public EnemyGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
   
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

        if (enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position, enemy.transform.position) < 1 || enemy.stats.isDamaged)
            stateMachine.ChangeState(enemy.battleState);
        //return;
    }
}
