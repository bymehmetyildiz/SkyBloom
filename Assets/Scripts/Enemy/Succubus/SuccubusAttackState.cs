using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusAttackState : EnemyState
{
    private Succubus enemy;
    public SuccubusAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(56, null);
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }


        if (triggerCalled )        
        {
            if (!enemy.IsPlayerDetected() || enemy.IsPlayerDetected().distance >= enemy.agroDistance)
                stateMachine.ChangeState(enemy.idleState);
        }
    }
}
