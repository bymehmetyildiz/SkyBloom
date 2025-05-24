using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThunder : MonoBehaviour
{
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround; 
    [SerializeField] private Enemy[] enemyPrefab;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;
    private Rigidbody2D rb;
    private Succubus succubus;
    [SerializeField] private AudioSource thuınder;
   


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        succubus = FindObjectOfType<Succubus>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        thuınder.Play();
       
    }

    private void Update()
    {
        if (IsGroundDetected())
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("Strike", true);
        }
    }

    private bool IsGroundDetected() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    private void OnDrawGizmos() => Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));

    public void SpawnEnemy()
    {
        capsuleCollider.enabled = false;
        Enemy newEnemy =  Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        succubus.currentEnemy = newEnemy;
        Destroy(gameObject, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() != null)
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(15);
        


    }

}
