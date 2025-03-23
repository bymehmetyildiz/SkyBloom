using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomBattleState : EnemyState
{
    private Mushroom mushroom;
    private int moveDir;

    public MushroomBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mushroom = _mushroom;
    }

    public override void Enter()
    {
        base.Enter();

        mushroom.stats.isDamaged = false;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(mushroom.moveState);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > mushroom.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < mushroom.transform.position.x)
            moveDir = -1;

        mushroom.SetVelocity(mushroom.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!mushroom.IsGroundDetected())
        {
            mushroom.Flip();
            stateMachine.ChangeState(mushroom.moveState);
            return;
        }


        if (mushroom.IsPlayerDetected())
        {
            stateTimer = mushroom.agroTime;

            if (mushroom.IsPlayerDetected().distance <= mushroom.attackDistance)
            {                
                stateMachine.ChangeState(mushroom.attackState);
               
            }
        }

        if (mushroom.IsWallDetected() || !mushroom.IsGroundDetected() || mushroom.IsDangerDetected())
            stateMachine.ChangeState(mushroom.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, mushroom.transform.position) > 10)
            stateMachine.ChangeState(mushroom.idleState);
    }
}
