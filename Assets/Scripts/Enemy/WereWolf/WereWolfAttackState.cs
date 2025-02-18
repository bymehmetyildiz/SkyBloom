using UnityEngine;

public class WereWolfAttackState : EnemyState
{
    private WereWolf wereWolf;
    private Player player;

   

    public WereWolfAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player;
        wereWolf.SetZeroVelocity();

        if (wereWolf.comboCounter > 2)
            wereWolf.comboCounter = 0;

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
        Debug.Log(wereWolf.comboCounter);

        if (player.gameObject.GetComponent<PlayerStats>().isDead)            
            stateMachine.ChangeState(wereWolf.idleState);

        if (triggerCalled)
        {
            if (wereWolf.IsPlayerDetected() &&
                       wereWolf.IsPlayerDetected().distance <= 1.9f)
            {
                if (wereWolf.comboCounter > 2)
                    wereWolf.comboCounter = 0;

                wereWolf.anim.SetInteger("ComboCounter", wereWolf.comboCounter);
            }
            else
            {
                stateMachine.ChangeState(wereWolf.battleState);
            }
        }

    }
}
