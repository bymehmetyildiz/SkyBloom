using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AudioSource explosion;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Hit", true);
        explosion.Play();
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
