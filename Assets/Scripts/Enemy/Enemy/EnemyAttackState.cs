using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private StandartEnemy enemy;
 
    public EnemyAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, StandartEnemy _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();     
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
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
            if (!enemy.IsPlayerDetected() || enemy.IsPlayerDetected().distance >= enemy.agroDistance)
                stateMachine.ChangeState(enemy.idleState);

            triggerCalled = false;
        }
    }
}
