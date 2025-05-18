using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDisc: MonoBehaviour
{
    private int direction;
    private int speed;
    private Rigidbody2D rb;
    private GameObject hitEffect;
    Player player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyDisc());
    }

    private IEnumerator DestroyDisc()
    {
        yield return new WaitForSeconds(2);
        AudioManager.instance.StopSFX(18);
        Destroy(gameObject);
    }


    public void Setup(int _direction, int _speed, GameObject _hitEffect, Player _player)
    {
        direction = _direction;
        speed = _speed;
        hitEffect = _hitEffect;
        player = _player;
    }

    
    void Update()
    {
        rb.velocity = Vector2.right * direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null || collision.GetComponent<DeadZone>() != null)
            return;

        if (collision.GetComponent<Enemy>() != null)
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();

            player.stats.DoMagicalDamage(enemy);

        }
        var collisionPoint = collision.ClosestPoint(transform.position);
        Instantiate(hitEffect, collisionPoint, Quaternion.identity);
        AudioManager.instance.StopSFX(18);
        AudioManager.instance.PlaySFX(17, null);
        Destroy(gameObject);

    }
}
