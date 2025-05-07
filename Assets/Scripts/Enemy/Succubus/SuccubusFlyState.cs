using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusFlyState : EnemyState
{
    private Succubus enemy;
    public SuccubusFlyState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 10;
        enemy.StartCoroutine(enemy.SpawnThunder());
    }

    public override void Exit()
    {
        base.Exit();        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
        else if (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();

        if (enemy.transform.position.y < enemy.riseDistance)
            enemy.SetVelocity(rb.velocity.x, enemy.flySpeed);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0 && enemy.currentEnemy == null)
            stateMachine.ChangeState(enemy.downState);

        
        
    }
}
