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

    //Movement
    public void OnMoveButtonDown(float _xInput) => xInput = _xInput;
    public void OnMoveButtonUp() => xInput = 0f;
    ///////


    //Jump
    public void MobileJumpState()
    {
        if(!isJumped)
        {
            isJumped = true;
            yInput = 1f;
        }
    }
    ///////
    

    //Dash
    public void MobileDashStateDown()
    {
        if (!isDashed)
        {
            isDashed = true; 
        }

    }

    public void MobileDashStateUp()
    {
        if (isDashed)
        {
            isDashed = false;
        }
    }
    ///////
   
    //Block
    public void MobileBlockStateDown()
    {
        if(PlayerManager.instance.player.isBusy == false)
            PlayerManager.instance.player.stateMachine.ChangeState(PlayerManager.instance.player.blockState);
    }

    //End Block
}
