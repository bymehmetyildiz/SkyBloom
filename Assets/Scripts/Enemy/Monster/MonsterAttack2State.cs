using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack2State : EnemyState
{
    private Monster enemy;
    private int moveDir;
    public MonsterAttack2State(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Monster _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (player.transform.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < enemy.transform.position.x)
            moveDir = -1;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.idleState);
    }
}
