using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class MaulerBattleState : EnemyState
{
    private Mauler mauler;
    private int moveDir;
    private float spAttackCooldown = 2.0f; // Adjust cooldown time as needed
    private float spAttackTimer = 0f;
    private int spAttackChance; // Adjust the chance (0.0 to 1.0)


    public MaulerBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mauler = _mauler;
    }

    public override void Enter()
    {
        base.Enter();

        mauler.stats.isDamaged = false;

        spAttackTimer = spAttackCooldown;

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

        if (!mauler.IsGroundDetected() || mauler.IsDangerDetected())
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
        }

        if (mauler.IsWallDetected() || stateTimer < 0 || Vector2.Distance(player.transform.position, mauler.transform.position) > 10)
            stateMachine.ChangeState(mauler.idleState);

        // SpAttack
        spAttackTimer -= Time.deltaTime;

        if (spAttackTimer <= 0)
        {
            spAttackChance = Random.Range(0, 5);

            if (spAttackChance <= 4) // Random chance check
            {
                stateMachine.ChangeState(mauler.spAttackState);
                spAttackTimer = spAttackCooldown; // Reset cooldown
            }
            else
            {
                spAttackTimer = spAttackCooldown; // Reset cooldown even if not triggered
            }

        }

    }
}
