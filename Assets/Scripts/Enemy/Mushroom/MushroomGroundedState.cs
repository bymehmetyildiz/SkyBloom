using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGroundedState : EnemyState
{
    protected Mushroom mushroom;

    public MushroomGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        mushroom = _mushroom;
    }

    public override void Enter()
    {
        base.Enter();
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

        if ((mushroom.IsPlayerDetected() || Vector2.Distance(player.transform.position, mushroom.transform.position) < 1))
            stateMachine.ChangeState(mushroom.battleState);
    }
}
