using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;    
    [SerializeField] private string ground = "Ground";
    private Transform player;
    private Vector2 playerPositionAtSpawn;
    [SerializeField] private float maxHeight;
    private BoxCollider2D bc;
    private bool canTrap;
    [SerializeField] private AudioSource trapSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = PlayerManager.instance.player.transform;
        playerPositionAtSpawn = player.position;
        maxHeight = player.position.y + 5;        
        bc = GetComponent<BoxCollider2D>();
        ShootProjectile();
        
    }

    public void ShootProjectile()
    {
        // Calculate the trajectory to hit exactly at the player's position
        Vector2 targetPos = playerPositionAtSpawn;
        Vector2 startPos = transform.position;

        // Horizontal distance to target
        float displacementX = targetPos.x - startPos.x;

        // Vertical distance to target (could be positive or negative)
        float displacementY = targetPos.y - startPos.y;

        // Gravity magnitude from Unity's physics
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // Calculate time to reach target based on desired height
        // This is an approximation that ensures a nice arc
        float heightDifference = maxHeight - startPos.y;
        float timeToTarget = Mathf.Sqrt(2 * heightDifference / gravity) +
                           Mathf.Sqrt(2 * (heightDifference - displacementY) / gravity);

        // Calculate required velocities
        float velocityX = displacementX / timeToTarget;

        // Calculate initial velocity Y using the time and displacement
        float velocityY = displacementY / timeToTarget + 0.5f * gravity * timeToTarget;

        // Apply the calculated velocity     
        rb.velocity = new Vector2(velocityX, velocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ground))
        {
            if (rb.velocity.y < 0)
            {
                animator.SetBool("Up", true);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                bc.size = new Vector2(0.25f, 0.5f);
                transform.position += Vector3.up * 0.85f;
                canTrap = true;
                trapSFX.Play();
            }
        }

        if (collision.GetComponent<Player>() != null)
        {
            if (canTrap && collision.GetComponent<Player>().IsGroundDetected())
            {
                Player player = collision.GetComponent<Player>();
                player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
                player.isBusy = true;
                player.SetZeroVelocity();
                canTrap = false;
                trapSFX.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.player.isBusy = false;   
        }
    }

    private IEnumerator SetTrapDown()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("Up", false);        
        yield return new WaitForSeconds(0.5f);
        PlayerManager.instance.player.isBusy = false;

    }

    private void DestroyIvy() => Destroy(gameObject);

}
