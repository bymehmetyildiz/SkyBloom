using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Screen Shake")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    [SerializeField] private Vector3 shakePower;
    [SerializeField] private AudioSource explosion;

    void Start()
    {
        screenShake = GetComponent<CinemachineImpulseSource>();
        ScreenShake();
    }

    
    public void DestroyGameObject() => Destroy(gameObject);

    public void DamageEntities()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 2.5f);

        foreach (Collider2D collision in collisions)
        {
            Entity entity = collision.GetComponent<Entity>();
            EntityStats entityStats = collision.GetComponent<EntityStats>();
            if (entity != null && entityStats != null)
            {
                entity.SetupKnockBackDir(transform);

                // Set a strong knockback for explosion (tune as needed)
                entity.SetupKnockbackPower(new Vector2(8, 8)); // Example: (8,8) is strong, adjust as needed
                
                entityStats.TakeDamage(150);
                entity.StartCoroutine(entity.KnockBack());
                
            }

            if (collision.GetComponent<Explosive>() != null)
            {
                collision.GetComponent<Explosive>().Explode();
            }
        }
    }
    public void ScreenShake()
    {
        screenShake.m_DefaultVelocity = new Vector3(shakePower.x , shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
        explosion.Play();
    }
}
