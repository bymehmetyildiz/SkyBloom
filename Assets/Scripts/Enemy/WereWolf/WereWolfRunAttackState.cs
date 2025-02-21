using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WereWolfRunAttackState : EnemyState
{
    private WereWolf wereWolf;
    private bool isPalyerDetected;
    private float jumpDelay = 0.25f;
    private bool jumpTriggered = false;

    public WereWolfRunAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();
        wereWolf.SetupKnockBackDir(player.transform);
        isPalyerDetected = false;
    }

  
    public override void Exit()
    {
        base.Exit();
        jumpTriggered = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!jumpTriggered)
        {
            jumpDelay -= Time.deltaTime;
            if (jumpDelay <= 0)
            {
                // Jump logic here
                wereWolf.SetVelocity(wereWolf.moveSpeed * 3 * -wereWolf.knockBackDir, 3);
                jumpTriggered = true;
            }
           
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isPalyerDetected)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(wereWolf.attackCheck.position, wereWolf.attackCheckDistance);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Player>() != null)
                {
                    if (player.stateMachine.currentState == player.blockState)
                        stateMachine.ChangeState(wereWolf.stunState);

                    else
                    {
                        PlayerStats playerStats = colliders[i].GetComponent<PlayerStats>();
                        wereWolf.entityStats.DoDamage(playerStats);
                        isPalyerDetected = true;
                        break;
                    }
                }
            }
        }


        if (triggerCalled)
            stateMachine.ChangeState(wereWolf.idleState);
    }

    
}
