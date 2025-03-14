using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Arrow,
    Rock
}

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private GameObject hitParticle;

    [SerializeField] private int damage;
    [SerializeField] private string targetLayerName = "Player";

    public float xVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        flipped = false;
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
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Player player = collision.GetComponent<Player>();

                if (player.stateMachine.currentState == player.blockState)
                {
                    Destroy(gameObject);
                    Instantiate(hitParticle, transform.position, Quaternion.identity);
                    return;
                }
            }

            if (projectileType == ProjectileType.Rock)
                Explode(collision);
            else
                Stuck(collision);

            collision.GetComponent<EntityStats>()?.TakeDamage(damage);

           

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (projectileType == ProjectileType.Rock)
                Explode(collision);
            else
                Stuck(collision);
        }
       
    }

    private void Explode(Collider2D collision)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("Explode", true);
        xVelocity = 0;
        canMove = false;
    }

    private void Stuck(Collider2D collision)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        xVelocity = 0;
        canMove = false;
        transform.parent = collision.gameObject.transform;
        Destroy(gameObject);
        Instantiate(hitParticle, transform.position, Quaternion.identity);
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
