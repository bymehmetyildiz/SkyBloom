using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_AdIconController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPointerOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        PlayerManager.instance.player.isBusy = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        PlayerManager.instance.player.isBusy = false;
    }
}
