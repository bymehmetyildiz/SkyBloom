using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WolfBattleState : EnemyState
{
    private Wolf wolf;
    private int moveDir;
    private float distanceToPlayer;

    public WolfBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wolf = _wolf;
    }

    public override void Enter()
    {
        base.Enter();

        wolf.stats.isDamaged = false;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(wolf.moveState);

        wolf.StopCounterAttack();
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

       distanceToPlayer = Mathf.Abs(player.transform.position.x - wolf.transform.position.x);

        if (distanceToPlayer < 0.2f)
            return;

        if (player.transform.position.x > wolf.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < wolf.transform.position.x)
            moveDir = -1;

        wolf.SetVelocity(wolf.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();        

        if (!wolf.IsGroundDetected())
        {
            wolf.Flip();
            stateMachine.ChangeState(wolf.moveState);
            return;
        }


        if (wolf.IsPlayerDetected() || (distanceToPlayer <= wolf.attackDistance && player.IsGroundDetected()))
        {
            stateTimer = wolf.agroTime;

            if (wolf.IsPlayerDetected().distance <= wolf.attackDistance)
            {
                //if(CanAttack())
                stateMachine.ChangeState(wolf.attackState);
                //else
                //    stateMachine.ChangeState(bandit.idleState);
            }
        }
        else
        {           
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, wolf.transform.position) > 10)
                stateMachine.ChangeState(wolf.idleState);
        }

        if (wolf.IsWallDetected() || !wolf.IsGroundDetected() || wolf.IsDangerDetected())
            stateMachine.ChangeState(wolf.idleState);

       

    }
}
