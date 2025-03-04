using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
  
    private float lastAtttackTime;
    private float comboWindow = 2;


    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0; // This is to prevent a bug

        if (player.comboCounter > 3 || Time.time >= lastAtttackTime + comboWindow)
            player.comboCounter = 0;

        player.anim.SetInteger("ComboCounter", player.comboCounter);

        stateTimer = 0.1f;

        float attackDirection = player.facingDir;

        if (xInput != 0)
            attackDirection = xInput;

        player.SetVelocity(player.attackMove * attackDirection, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.CheckBusy(0.25f));
        player.stunTrigger = false;
        player.comboCounter++;
        lastAtttackTime = Time.time;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckDistance);

        foreach (var hit in colliders)
        {
            //if (hit.GetComponent<Projectile>() != null)
            //    hit.GetComponent<Projectile>().Flip();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (stateTimer < 0)
            rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

       


        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
