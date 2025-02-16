using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttackState : EnemyState
{
    private Wolf wolf;
    private Transform player;

    public WolfAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Wolf _wolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wolf = _wolf;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        wolf.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        wolf.lastAttackTime = Time.time;
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
            if (Vector2.Distance(player.transform.position, wolf.transform.position) > wolf.attackDistance)
                stateMachine.ChangeState(wolf.battleState);
        }
    }
}
