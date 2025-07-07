using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skillManager.swordSkill.ActivateDots(true);
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine(player.CheckBusy(0.2f));
        player.skillManager.swordSkill.UseSkill();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.idleState);
            AudioManager.instance.PlaySFX(16, null);
        }
          
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePos.x && player.facingDir == 1)
            player.Flip();
        else if(player.transform.position.x < mousePos.x && player.facingDir == -1)
            player.Flip();

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.touches[0];
        //    Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

        //    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        //    {
        //        if (player.transform.position.x > touchWorldPos.x && player.facingDir == 1)
        //            player.Flip();
        //        else if (player.transform.position.x < touchWorldPos.x && player.facingDir == -1)
        //            player.Flip();
        //    }
        //    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        //    {
        //        stateMachine.ChangeState(player.idleState);
        //        AudioManager.instance.PlaySFX(16, null);
        //    }
        //}
        
    }


}
