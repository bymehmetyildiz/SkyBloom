using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : MonoBehaviour
{
    private HingeJoint2D hg;

    void Start()
    {
        hg = GetComponent<HingeJoint2D>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            hg.enabled = false;
        }
    }
}
