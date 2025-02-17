using UnityEngine;

public class BanditBattleState : EnemyState
{
    private Bandit bandit;
    private Transform player;
    private int moveDir;

    public BanditBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Bandit _bandit) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.bandit = _bandit;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(bandit.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > bandit.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < bandit.transform.position.x)
            moveDir = -1;

        bandit.SetVelocity(bandit.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!bandit.IsGroundDetected())
        {
            bandit.Flip();
            stateMachine.ChangeState(bandit.moveState);
            return;
        }


        if (bandit.IsPlayerDetected())
        {
            stateTimer = bandit.agroTime;

            if (bandit.IsPlayerDetected().distance <= bandit.attackDistance)
            {
                //if(CanAttack())
                stateMachine.ChangeState(bandit.attackState);
                //else
                //    stateMachine.ChangeState(bandit.idleState);
            }
        }

        if(bandit.IsWallDetected() || !bandit.IsGroundDetected())
            stateMachine.ChangeState(bandit.idleState);

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, bandit.transform.position) > 10)
            stateMachine.ChangeState(bandit.idleState);

    }

    //private bool CanAttack()
    //{
    //    if (Time.time >= bandit.lastAttackTime + bandit.attackCoolDown)
    //    {
    //        bandit.lastAttackTime = Time.time;
    //        return true;
    //    }

    //    return false;
    //}

}
