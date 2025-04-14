using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : MonoBehaviour
{
    private HingeJoint2D hg;
    private GameObject wheelBarrow;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SwordSkillController>() != null)
        {
            Destroy(gameObject);
        }

    }
}
