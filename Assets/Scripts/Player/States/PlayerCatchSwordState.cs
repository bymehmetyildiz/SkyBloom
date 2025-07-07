using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(17, null);
        AudioManager.instance.StopSFX(18);

        if (player.sword == null)
        {
            Debug.LogWarning("Trying to catch a sword that no longer exists.");
            stateMachine.ChangeState(player.idleState);
            return;
        }

        sword = player.sword.transform;

        if (player.transform.position.x > sword.position.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.facingDir == -1)
            player.Flip();

        rb.velocity = new Vector2(player.catchImpact * -player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine(player.CheckBusy(0.15f));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
