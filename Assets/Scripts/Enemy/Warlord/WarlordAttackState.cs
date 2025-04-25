using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlordAttackState : EnemyState
{
    private Warlord enemy;
    public WarlordAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Warlord _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.comboCounter > 2)
            enemy.comboCounter = 0;

        enemy.anim.SetInteger("Combo", enemy.comboCounter);

        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.comboCounter++;
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.idleState);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.waitState);
        
    }
}
