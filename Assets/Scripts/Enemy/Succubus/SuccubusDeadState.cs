using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusDeadState : EnemyState
{
    
    private Succubus enemy;
    public SuccubusDeadState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Succubus _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(57, null);

    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
