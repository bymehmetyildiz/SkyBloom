using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRainCastState : PlayerState
{
    public PlayerRainCastState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skillManager.rainSkill.UseSkill();
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
        {
            player.skillManager.rainSkill.canRain = true;
            stateMachine.ChangeState(player.idleState);
        }

           
    }
}
