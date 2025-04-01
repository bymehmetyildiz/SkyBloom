using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            Player player = collision.GetComponent<Player>();

            if (player.stateMachine.currentState == player.dashState || player.stateMachine.currentState == player.dashAttackState)
                return;

            damage = (int)(player.stats.maxHealth.GetValue() * 0.15f);
            player.stats.TakeDamage(damage);
        }

    }
}
