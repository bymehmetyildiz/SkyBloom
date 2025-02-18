using UnityEngine;

public class WereWolfAttackState : EnemyState
{
    private WereWolf wereWolf;
    private Transform player;

   

    public WereWolfAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, WereWolf _wereWolf) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        wereWolf = _wereWolf;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        wereWolf.SetZeroVelocity();

        if (wereWolf.comboCounter > 2)
            wereWolf.comboCounter = 0;

        wereWolf.anim.SetInteger("ComboCounter", wereWolf.comboCounter);

    }

    public override void Exit()
    {
        base.Exit();
        wereWolf.lastAttackTime = Time.time;
        wereWolf.StopCounterAttack();

        wereWolf.comboCounter++;
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
            stateMachine.ChangeState(wereWolf.battleState);           
        }

    }
}
