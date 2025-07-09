using UnityEngine;
using UnityEngine.UI;

public class AimPanel : MonoBehaviour
{
    private Player player;
    private RectTransform rectTransform;
    private Vector2 finalDir;

    // Long press variables
    [SerializeField] private float aimHoldDelay = 0.3f; // seconds to trigger aim
    private float holdTimer = 0f;
    private bool isHolding = false;
    private bool aimStarted = false;

    void Start()
    {
        player = PlayerManager.instance.player;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
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
                isHolding = true;
                holdTimer = 0f;
                aimStarted = false;
            }

            if (isHolding && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                holdTimer += Time.deltaTime;
                if (!aimStarted && holdTimer >= aimHoldDelay)
                {
                    // Only start aiming after the delay
                    if (HasNoSword() && player.stats.currentMagic >= swordSkill.magicAmount)
                    {
                        player.stateMachine.ChangeState(player.aimSwordState);
                        swordSkill.ActivateDots(true);
                        player.SetZeroVelocity();
                        aimStarted = true;
                    }
                }

                if (aimStarted && HasNoSword())
                {
                    swordSkill.ActivateDots(true);
                    player.SetZeroVelocity();

                    Vector2 aimDir = AimDirection().normalized;
                    finalDir = aimDir * swordSkill.launchForce.magnitude;

                    for (int i = 0; i < swordSkill.dots.Length; i++)
                        swordSkill.dots[i].transform.position = DotsPosition(i * swordSkill.spaceBetweenDots);

                    // Flip based on finger
                    if (player.transform.position.x >= touchWorldPos.x && player.facingDir == 1)
                        player.Flip();
                    else if (player.transform.position.x < touchWorldPos.x && player.facingDir == -1)
                        player.Flip();
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isHolding = false;
                holdTimer = 0f;

                if (aimStarted)
                {
                    swordSkill.ActivateDots(false);
                    swordSkill.finalDir = finalDir;
                    swordSkill.CreateSword();
                    AudioManager.instance.PlaySFX(16, null);
                    player.stateMachine.ChangeState(player.idleState);
                }
            }
        }
        else
        {
            // Reset if no touch
            isHolding = false;
            holdTimer = 0f;
            aimStarted = false;
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
        var swordSkill = player.skillManager.swordSkill;
        Vector2 aimDir = AimDirection().normalized;
        Vector2 force = aimDir * swordSkill.launchForce.magnitude; // Use the same as finalDir

        return (Vector2)player.transform.position
            + force * t
            + 0.5f * (Physics2D.gravity * swordSkill.swordGravity) * (t * t);
    }

    private bool HasNoSword()
    {
        if (!player.sword)
            return true;

        player.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}