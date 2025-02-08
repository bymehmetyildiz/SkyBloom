using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_AnimationTrigger : MonoBehaviour
{
    private Bandit bandit => GetComponentInParent<Bandit>();

    public void AnimationTrigger()
    {
        bandit.AnimationTrigger();
    }

    public void TriggerDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bandit.attackCheck.position, bandit.attackCheckDistance);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {
                PlayerStats playerStats = colliders[i].GetComponent<PlayerStats>();
                bandit.entityStats.DoDamage(playerStats, bandit.facingDir);
            }

        }
    }

    private void StartCounterAttack() => bandit.StartCounterAttack();
    private void StopCounterAttack() => bandit.StopCounterAttack();


}
