using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGroundedState : EnemyState
{
    protected Eye eye;
    public EyeGroundedState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Eye _eye) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.eye = _eye;
    }

    public override void Update()
    {
        base.Update();

        if ((eye.IsPlayerDetected() || Vector2.Distance(player.transform.position, eye.transform.position) < 1))
            stateMachine.ChangeState(eye.battleState);
    }
}
