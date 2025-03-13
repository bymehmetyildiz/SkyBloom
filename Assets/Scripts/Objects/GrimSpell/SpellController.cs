using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [Header("Screen Shake")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    [SerializeField] private Vector3 shakePower;

    private void Start()
    {
        screenShake = GetComponent<CinemachineImpulseSource>();
    }

    private void DestroySpell()
    {

        Destroy(gameObject);
    }

    // Trigger Damage
    public void TriggerDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {
                PlayerStats playerStats = colliders[i].GetComponent<PlayerStats>();
                if (playerStats.isDead)
                    return;

                playerStats.TakeDamage(15);
            }
        }

        screenShake.m_DefaultVelocity = new Vector3(shakePower.x, shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }

}
