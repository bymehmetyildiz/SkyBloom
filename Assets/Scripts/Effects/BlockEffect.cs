using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEffect : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DestroyEffect()
    {
        Destroy(gameObject);
    }

}
