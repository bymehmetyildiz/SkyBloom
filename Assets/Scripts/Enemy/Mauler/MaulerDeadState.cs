using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaulerDeadState : EnemyState
{
    public MaulerDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
    }
}
