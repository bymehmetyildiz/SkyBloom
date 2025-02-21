using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        wereWolf.SpawnBlockEffect(player.attackCheck);
        wereWolf.shiledIcon.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        wereWolf.shiledIcon.SetActive(false);
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
            if (!wereWolf.IsPlayerDetected())
                wereWolf.Flip();
            else if (wereWolf.IsPlayerDetected())
                stateMachine.ChangeState(wereWolf.runAttackState);
           
        }


    }
}
