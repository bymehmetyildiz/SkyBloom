using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SawController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;    
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private int damage;
    private Transform targetPosition;
    [SerializeField] private AudioSource sawTrapSFX;

    void Start()
    {
        targetPosition = pointA;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition.position) < 0.1f)
        {
            targetPosition = (targetPosition == pointA) ? pointB : pointA;
        }

        float distance = Vector2.Distance(PlayerManager.instance.player.transform.position, transform.position);

        if (distance <= sawTrapSFX.maxDistance)
        {
            if (!sawTrapSFX.isPlaying)
            {
                sawTrapSFX.Play();
            }
        }
        else
        {
            if (sawTrapSFX.isPlaying)
            {
                sawTrapSFX.Stop();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerStats>() != null)
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();

            if (player.isDead)
                return;

            player.TakeDamage(damage);
        }
    }
}
