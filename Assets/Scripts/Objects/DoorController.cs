using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private BoxCollider2D bc;
    private Animator anim;
    public bool isOpen;
    
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        if (isOpen)
            StartCoroutine(OpenGate(0));
        else
            StartCoroutine(CloseGate(0));
    }

    public IEnumerator OpenGate(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        anim.SetBool("Open", true);
        bc.enabled = false;
        isOpen = true;
    }
   
    public IEnumerator CloseGate(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        anim.SetBool("Open", false);
        bc.enabled = true;
        isOpen = false;
    }

}
