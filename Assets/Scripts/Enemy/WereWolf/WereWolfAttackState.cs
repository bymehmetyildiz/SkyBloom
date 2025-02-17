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
    }

    public override void Exit()
    {
        base.Exit();
        wereWolf.lastAttackTime = Time.time;
        wereWolf.StopCounterAttack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();


        if (triggerCalled || !wereWolf.IsPlayerDetected() || Vector2.Distance(player.transform.position, wereWolf.transform.position) > 2.2f)
        {
            stateMachine.ChangeState(wereWolf.battleState);
        }



    }
}
