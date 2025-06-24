using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class AimButton : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    public float areaWidth = 200f;  // Width of the allowed area in UI units (pixels)
    public float areaHeight = 200f; // Height of the allowed area in UI units (pixels)
    public Transform playerTransform; // Assign the player tran

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        playerTransform = PlayerManager.instance.player.transform; 
    }

    
    void Update()
    {
        if (Input.touchCount > 0 && playerTransform != null)
        {
            Touch touch = Input.touches[0];

            // 1. Get player's world position in screen space
            Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(playerTransform.position);

            // 2. Define the rectangle in screen space
            float halfW = areaWidth * 0.5f;
            float halfH = areaHeight * 0.5f;
            Rect allowedRect = new Rect(
                playerScreenPos.x - halfW,
                playerScreenPos.y - halfH,
                areaWidth,
                areaHeight
            );

            // 3. Clamp the touch position to the allowed rectangle
            Vector2 clampedScreenPos = new Vector2(
                Mathf.Clamp(touch.position.x, allowedRect.xMin, allowedRect.xMax),
                Mathf.Clamp(touch.position.y, allowedRect.yMin, allowedRect.yMax)
            );

            // 4. Convert clamped screen position to UI anchored position
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                clampedScreenPos,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out anchoredPos);

            rectTransform.anchoredPosition = anchoredPos;
        }
    }
}
