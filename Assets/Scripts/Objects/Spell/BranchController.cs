using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int damage;
    [SerializeField] private Transform damageCheck;
    [SerializeField] private float damageCheckDist;    

    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 4);
    }

    
    void Update()
    {
        
    }

    private void Damage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(damageCheck.position, damageCheckDist);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {
                Player player = colliders[i].GetComponent<Player>();
                PlayerStats playerStats = colliders[i].GetComponent<PlayerStats>();

                if (playerStats.isDead)
                    return;

                player.SetupKnockBackDir(transform);
                if(!player.isBusy)
                    player.SetupKnockbackPower(new Vector2(3, 10));

                playerStats.TakeDamage(damage);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damageCheck.position, damageCheckDist);
    }

}
