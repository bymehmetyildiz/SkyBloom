using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(player.knockBackPower.x, player.knockBackPower.y);
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
