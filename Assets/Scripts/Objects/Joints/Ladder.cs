using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject chain;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsChainDestroyed())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private bool IsChainDestroyed()
    {
        if(chain == null)
            return true;

        return false;
    }

}
