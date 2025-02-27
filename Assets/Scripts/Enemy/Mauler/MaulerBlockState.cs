using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerBlockState : EnemyState
{
    private Mauler mauler;
    public MaulerBlockState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mauler _mauler) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mauler = _mauler;
    }

    public override void Enter()
    {
        base.Enter();

        mauler.SpawnBlockEffect(player.attackCheck);
        mauler.shiledIcon.SetActive(true);
        if (!mauler.IsPlayerDetected())
            mauler.Flip();
    }

    public override void Exit()
    {
        base.Exit();
        mauler.shiledIcon.SetActive(false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(mauler.idleState);
    }
}
