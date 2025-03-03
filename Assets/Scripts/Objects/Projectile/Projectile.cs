using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string targetLayerName = "Player";

    [SerializeField] private float xVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    private void TriggerCalled()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            collision.GetComponent<EntityStats>()?.TakeDamage(damage);
            Explode(collision);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Explode(collision);
    }

    private void Explode(Collider2D collision)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("Explode", true);
        xVelocity = 0;
        canMove = false;
    }

    public void Flip()
    {
        if (flipped)
            return;

        xVelocity = -1 * xVelocity;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayerName = "Enemy";
    }

}
