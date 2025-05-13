using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomBattleState : EnemyState
{
    private Mushroom mushroom;
    private int moveDir;
    private float distanceToPlayer;

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

        distanceToPlayer = Mathf.Abs(player.transform.position.x - mushroom.transform.position.x);

        if (distanceToPlayer < 0.2f)
            return;

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


        if (mushroom.IsPlayerDetected() || (distanceToPlayer <= mushroom.attackDistance && player.IsGroundDetected()))
        {
            stateTimer = mushroom.agroTime;

            if (mushroom.IsPlayerDetected().distance <= mushroom.attackDistance)
            {                
                stateMachine.ChangeState(mushroom.attackState);
               
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, mushroom.transform.position) > 10)
                stateMachine.ChangeState(mushroom.idleState);
        }

        if (mushroom.IsWallDetected() || !mushroom.IsGroundDetected() || mushroom.IsDangerDetected())
            stateMachine.ChangeState(mushroom.idleState);
    }
}
