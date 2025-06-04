using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] private HingeJoint2D[] joints;
    
    void Start()
    {
        joints = GetComponentsInChildren<HingeJoint2D>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SwordSkillController>() != null)
        {
            collision.GetComponent<SwordSkillController>().ReturnSword();
            DestroyChain();
            
        }

    }

    public void DestroyChain()
    {
        foreach (var joint in joints)
        {
            Destroy(joint.gameObject);
        }
        Destroy(gameObject);
    }
}
