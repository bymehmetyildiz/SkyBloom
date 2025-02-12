using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTrigger : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    public void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    public void TriggerDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckDistance);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {
                PlayerStats playerStats = colliders[i].GetComponent<PlayerStats>();
                enemy.entityStats.DoDamage(playerStats, enemy.facingDir);
            }

        }
    }

    private void StartCounterAttack() => enemy.StartCounterAttack();
    private void StopCounterAttack() => enemy.StopCounterAttack();


}
