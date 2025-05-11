using UnityEngine;

public class RogueBattleState : EnemyState
{
    private Rogue enemy;
    private int moveDir;
    public RogueBattleState(Enemy _baseEnemy, EnemyStateMachine _stateMachine, string _animBoolName, Rogue _enemy) : base(_baseEnemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.stats.isDamaged = false;

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.transform.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        var playerDetected = enemy.IsPlayerDetected();
        var playerDistance = Vector2.Distance(player.transform.position, enemy.transform.position);

        if (playerDetected)
        {
            stateTimer = enemy.agroTime;

            if (playerDetected.distance <= enemy.attackDistance)
            {
                stateMachine.ChangeState(enemy.dashState);
                return;
            }
        }
        else
        {
            enemy.Flip();
            playerDetected = enemy.IsPlayerDetected(); // Check again after flipping

            if (playerDetected && playerDetected.distance <= enemy.attackDistance)
            {
                stateTimer = enemy.agroTime;
                stateMachine.ChangeState(enemy.dashState);
                return;
            }
            else
            {
                stateMachine.ChangeState(enemy.ambushState);
                return;
            }
        }

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected() || enemy.IsDangerDetected() || stateTimer < 0 || playerDistance > 10f)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
