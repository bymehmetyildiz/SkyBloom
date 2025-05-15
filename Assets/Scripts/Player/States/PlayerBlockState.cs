using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    public PlayerBlockState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.parryDur;
        player.anim.SetBool("Parry", false);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckDistance);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Enemy>() != null)
            {
                if (colliders[i].GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("Parry", true);
                    player.fx.ScreenShake();
                    AudioManager.instance.PlaySFX(11, null);
                }

            }
             
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
