using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private Animator anim;
    

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Hit", true);        
    }

    
    void Update()
    {
        
    }

    public void DestroyHit()
    {
        if(transform.parent != null)
            Destroy(transform.parent.gameObject);

        Destroy(gameObject);
    }

}
