using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class WereWolfBlockState : EnemyState
{
    private WereWolf wereWolf;   

    public WereWolfBlockState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
        
    }

    public override void Enter()
    {
        base.Enter();
        wereWolf.fx.ScreenShake();
        AudioManager.instance.PlaySFX(11, null);
        wereWolf.SpawnBlockEffect(player.attackCheck);
        wereWolf.shiledIcon.SetActive(true);
        if (!wereWolf.IsPlayerDetected())
            wereWolf.Flip();
    }

    public override void Exit()
    {
        base.Exit();
        wereWolf.shiledIcon.SetActive(false);
        wereWolf.canBeDamaged = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)        
            stateMachine.ChangeState(wereWolf.prepareState);
    }
}
