using UnityEngine;

public class AimButton : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    public float areaWidth = 200f;
    public float areaHeight = 200f;
    public Transform playerTransform;

    private bool isDragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        playerTransform = PlayerManager.instance.player.transform;
    }
    public bool IsTouchWithinArea(Vector2 touchPos)
    {
        if (playerTransform == null) return false;

        Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(playerTransform.position);
        Rect allowedRect = new Rect(
            playerScreenPos.x - areaWidth * 0.5f,
            playerScreenPos.y - areaHeight * 0.5f,
            areaWidth,
            areaHeight
        );

        return allowedRect.Contains(touchPos);
    }

    void Update()
    {
        if (Input.touchCount > 0 && playerTransform != null)
        {
            Touch touch = Input.touches[0];

            Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(playerTransform.position);
            float halfW = areaWidth * 0.5f;
            float halfH = areaHeight * 0.5f;
            Rect allowedRect = new Rect(
                playerScreenPos.x - halfW,
                playerScreenPos.y - halfH,
                areaWidth,
                areaHeight
            );

            if (allowedRect.Contains(touch.position))
            {
                // Begin drag
                if (touch.phase == TouchPhase.Began)
                {
                    isDragging = true;
                    MobileInput.Instance.isAim = true;
                    // Optionally, activate dots here if needed
                }

                // Dragging
                if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
                {
                    Vector2 clampedScreenPos = new Vector2(
                        Mathf.Clamp(touch.position.x, allowedRect.xMin, allowedRect.xMax),
                        Mathf.Clamp(touch.position.y, allowedRect.yMin, allowedRect.yMax)
                    );

                    Vector2 anchoredPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvas.transform as RectTransform,
                        clampedScreenPos,
                        canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                        out anchoredPos);

                    rectTransform.anchoredPosition = anchoredPos;

                    // Calculate drag direction in world space
                    Vector2 worldTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 dragDir = (worldTouchPos - (Vector2)playerTransform.position).normalized;
                    MobileInput.Instance.dragDirection = dragDir;

                    // Activate dots if not already active
                    // (Assuming SwordSkill is accessible, e.g., via PlayerManager)
                    PlayerManager.instance.player.skillManager.swordSkill.ActivateDots(true);
                }
            }

            // End drag
            if (isDragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                isDragging = false;
                MobileInput.Instance.isAim = false;
                PlayerManager.instance.player.skillManager.swordSkill.ActivateDots(false);
            }
        }
        else
        {
            // No touch, reset
            if (isDragging)
            {
                isDragging = false;
                MobileInput.Instance.isAim = false;
                PlayerManager.instance.player.skillManager.swordSkill.ActivateDots(false);
            }
        }
    }
}