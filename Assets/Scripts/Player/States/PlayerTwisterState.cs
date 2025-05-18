using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwisterState : PlayerState
{
    private bool isExpanding = false;
    public PlayerTwisterState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(20, null);
        player.cc.enabled = true;
        isExpanding = true; // Start expanding
        player.skillManager.twisterSkill.UseSkill();
    }

    public override void Exit()
    {
        base.Exit();
        player.cc.enabled = false;
        player.cc.radius = 0;
        isExpanding = false; // Stop expanding
        AudioManager.instance.StopSFX(20);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocity(xInput * player.moveSpeed/2, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (isExpanding)
        {
            player.cc.radius = Mathf.MoveTowards(player.cc.radius, 0.5f, 1.0f * Time.deltaTime);
        }

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

   
}
