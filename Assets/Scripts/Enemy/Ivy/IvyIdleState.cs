using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyIdleState : EnemyState
{
    private Ivy enemy;
    public IvyIdleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.isDamaged = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (enemy.transform.position.x > player.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
        else if (enemy.transform.position.x <= player.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.stats.isDamaged)
            stateMachine.ChangeState(enemy.hitState);

        if (GameManager.instance.isPlayerEnetered && Vector2.Distance(player.transform.position, enemy.transform.position) > 1 && player.IsGroundDetected())
            stateMachine.ChangeState(enemy.branchAttackState);

        if ((enemy.IsPlayerDetected() && Vector2.Distance(player.transform.position, enemy.transform.position) < 1))
            stateMachine.ChangeState(enemy.attackState);
    }
}
