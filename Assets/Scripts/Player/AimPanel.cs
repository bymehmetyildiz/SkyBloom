using UnityEngine;
using UnityEngine.UI;

public class AimPanel : MonoBehaviour
{
    private Player player;
    private RectTransform rectTransform;
    private Vector2 finalDir;



    void Start()
    {
        player = PlayerManager.instance.player;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Vector2 touchPos = touch.position;
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touchPos, Camera.main))
                return;

            var swordSkill = player.skillManager.swordSkill;

            if (swordSkill.swordType == SwordType.None || player.isBusy)
                return;

            if (touch.phase == TouchPhase.Began)
            {
                if (HasNoSword() && player.stats.currentMagic >= swordSkill.magicAmount)
                {
                    player.stateMachine.ChangeState(player.aimSwordState);
                    swordSkill.ActivateDots(true);
                    player.SetZeroVelocity();
                }
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (HasNoSword() )
                {
                    swordSkill.ActivateDots(true);
                    player.SetZeroVelocity();
                }


                Vector2 aimDir = AimDirection().normalized;
                finalDir = aimDir * swordSkill.launchForce.magnitude; // Use consistent force in all directions

                for (int i = 0; i < swordSkill.dots.Length; i++)
                {
                    swordSkill.dots[i].transform.position = DotsPosition(i * swordSkill.spaceBetweenDots);
                }

                // Flip based on finger
                if (player.transform.position.x >= touchWorldPos.x && player.facingDir == 1)
                    player.Flip();
                else if (player.transform.position.x < touchWorldPos.x && player.facingDir == -1)
                    player.Flip();
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                swordSkill.ActivateDots(false);
                swordSkill.finalDir = finalDir;
                swordSkill.CreateSword();
                AudioManager.instance.PlaySFX(16, null);
                player.stateMachine.ChangeState(player.idleState);
            }
        }
    }

    private Vector2 AimDirection()
    {        
        Vector3 playerPosition = player.transform.position;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        Vector2 dir = worldPos - playerPosition;
        return dir;
         
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position;
        position = (Vector2)player.transform.position + new Vector2(AimDirection().normalized.x * player.skillManager.swordSkill.launchForce.x,
                AimDirection().normalized.y * player.skillManager.swordSkill.launchForce.y) * t + 0.5f * (Physics2D.gravity * player.skillManager.swordSkill.swordGravity) * (t * t);
        return position;
    }

   
    private bool HasNoSword()
    {
        if (!player.sword)
            return true;

        player.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
