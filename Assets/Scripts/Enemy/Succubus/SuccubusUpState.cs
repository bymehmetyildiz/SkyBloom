using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusUpState : EnemyState
{
    private Succubus enemy;
    public SuccubusUpState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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

        enemy.SetVelocity(rb.velocity.x, enemy.flySpeed);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.transform.position.y >= enemy.riseDistance)
            stateMachine.ChangeState(enemy.flyState);
                
    }
}
