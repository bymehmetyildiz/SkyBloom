using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerBattleState : EnemyState
{
    private Mauler mauler;
    private int moveDir;
    

    public MaulerBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mauler = _mauler;
    }

    public override void Enter()
    {
        base.Enter();
        mauler.spTimer = mauler.spDuration;
        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(mauler.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > mauler.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < mauler.transform.position.x)
            moveDir = -1;

        mauler.SetVelocity(mauler.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        mauler.spTimer -= Time.deltaTime;

        if (!mauler.IsGroundDetected())
        {
            mauler.Flip();
            stateMachine.ChangeState(mauler.moveState);
            return;
        }

        if (mauler.IsPlayerDetected())
        {
            stateTimer = mauler.agroTime;

            if (mauler.IsPlayerDetected().distance <= mauler.attackDistance)
                stateMachine.ChangeState(mauler.attackState);
            else if(mauler.spTimer < 0)            
                stateMachine.ChangeState(mauler.spAttackState);
            
        }

        if (mauler.IsWallDetected() || !mauler.IsGroundDetected())
            stateMachine.ChangeState(mauler.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, mauler.transform.position) > 10)
            stateMachine.ChangeState(mauler.idleState);
    }
}
