using UnityEngine;

public class BanditAttackState : EnemyState
{
    private Bandit bandit;
    private Transform player;

    public BanditAttackState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit _bandit) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.bandit = _bandit;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        bandit.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        bandit.lastAttackTime = Time.time;
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
            if (Vector2.Distance(player.transform.position, bandit.transform.position) > bandit.attackDistance)
                stateMachine.ChangeState(bandit.battleState);
        }





    }
}
