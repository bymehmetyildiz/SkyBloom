using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance;

    public float xInput;
    public float yInput;
    public bool isPointerOver;
    public bool isJumped;
    public bool isDashed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isJumped = false;
        isDashed = false;
    }

    public void OnMoveButtonDown(float _xInput) => xInput = _xInput;
    public void OnMoveButtonUp() => xInput = 0f;
    public void MobileJumpState()
    {
        if(!isJumped)
        {
            isJumped = true;
            yInput = 1f;
        }
    }

    public void MobileDashState()
    {
        if (!isDashed)
        {
            isDashed = true; 
        }

    }


}
