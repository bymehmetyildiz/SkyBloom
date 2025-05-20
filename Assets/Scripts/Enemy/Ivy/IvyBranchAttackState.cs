using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyBranchAttackState : EnemyState
{
    private Ivy enemy;
    public IvyBranchAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Ivy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(36, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.isDamaged = false;
        AudioManager.instance.StopSFX(36);
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


        if ((enemy.IsPlayerDetected() && Vector2.Distance(player.transform.position, enemy.transform.position) < 2))
            stateMachine.ChangeState(enemy.attackState);

        if (triggerCalled && !player.isBusy && player.IsGroundDetected())
            stateMachine.ChangeState(enemy.trapState);
    }
}
