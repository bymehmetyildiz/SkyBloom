using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var stats = collision.gameObject.GetComponent<PlayerStats>() ?? (EntityStats)collision.gameObject.GetComponent<EnemyStats>();

        if (stats != null)
        {
            stats.Dead();
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

  

}
