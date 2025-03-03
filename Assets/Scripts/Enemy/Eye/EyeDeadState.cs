using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDeadState : EnemyState
{
    public EyeDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
    }
}
