using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDeadState : EnemyState
{
    private Wolf wolf;
    public WolfDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wolf = _wolf;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(8, wolf.transform);
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
