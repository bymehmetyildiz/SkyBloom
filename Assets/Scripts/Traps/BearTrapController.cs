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
                    PlayerStats player = colliders[i].GetComponent<PlayerStats>();
                    player.player.SetupKnockbackPower(transform.parent.up * 7);
                    player.TakeDamage(damage);
                    
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
