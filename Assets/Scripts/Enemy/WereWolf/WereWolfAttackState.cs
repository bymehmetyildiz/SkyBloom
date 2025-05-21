using UnityEngine;

public class WereWolfAttackState : EnemyState
{
    private WereWolf wereWolf; 

    public WereWolfAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();        
        wereWolf.SetZeroVelocity();
        AudioManager.instance.PlaySFX(44, null);
    }

    public override void Exit()
    {
        base.Exit();        
        wereWolf.StopCounterAttack();
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (player.gameObject.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(wereWolf.idleState);

        if (triggerCalled)
        {
            if (!wereWolf.IsPlayerDetected() || wereWolf.IsPlayerDetected().distance >= wereWolf.agroDistance)
                stateMachine.ChangeState(wereWolf.idleState);

            triggerCalled = false;
        }

    }
 
}
