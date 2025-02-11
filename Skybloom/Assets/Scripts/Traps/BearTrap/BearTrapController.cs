using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    private bool isTriggered;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    public void Trap()
    {
        if (isTriggered)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Player>() != null)
                {
                    //colliders[i].GetComponent<Enemy>().Damage(player.facingDir);
                    PlayerStats player = colliders[i].GetComponent<PlayerStats>();

                    int direction = 0;

                    if (player.transform.position.x > transform.position.x)
                        direction = 1;
                    else if (player.transform.position.x < transform.position.x)
                        direction = -1;

                    player.TakeDamage(damage, direction);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isTriggered = true;
            anim.SetBool("isTriggered", isTriggered);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }



}
