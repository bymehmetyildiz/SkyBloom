using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDeadState : EnemyState
{
    private Mushroom mushroom;
    public MushroomDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Mushroom _mushroom) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.mushroom = _mushroom;
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
    }
}
