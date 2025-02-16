using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfGroundedState : EnemyState
{
    protected Wolf wolf;
    protected Transform player;

    public WolfGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wolf = _wolf;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

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

        if ((wolf.IsPlayerDetected() || Vector2.Distance(player.transform.position, wolf.transform.position) < 1))
            stateMachine.ChangeState(wolf.battleState);
        //return;
    }
}
