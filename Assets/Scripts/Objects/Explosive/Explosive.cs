using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject ladder;
    [SerializeField] private GameObject destructible;

    void Start()
    {
        
    }

    private void Update()
    {
        if(IsGroundDetected())
            Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SwordSkillController>() != null)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, (Vector2)transform.position + Vector2.up, Quaternion.identity);
        if (ladder != null)
        {
            ladder.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        if (destructible != null && Vector2.Distance(transform.position, destructible.transform.position) < 3)
        {
            Debug.Log("Destructible distance: " + Vector2.Distance(transform.position, destructible.transform.position));
            Destroy(destructible);
        }
       
        Destroy(gameObject, 0.1f);
    }

    private bool IsGroundDetected() => Physics2D.Raycast(transform.position , Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * groundCheckDistance);
    }

}
