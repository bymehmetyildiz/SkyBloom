using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiscCastState : PlayerState
{
    public PlayerDiscCastState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skillManager.fireDiscSkill.UseSkill();
        AudioManager.instance.PlaySFX(18, player.transform);
    }

    public override void Exit()
    {
        base.Exit();
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
