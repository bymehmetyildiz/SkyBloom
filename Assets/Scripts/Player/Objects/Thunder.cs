using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private Player player;

    private Rigidbody2D rb;
    private float speed;
    

    public void Start()
    {        
        rb = GetComponent<Rigidbody2D>();      
    }

    public void SetUpThunder(float _speed, int _damage, Player _player)
    {
        speed = _speed;        
        player = _player;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed;

    }

    public void DestroyThunder() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {            
            player.stats.DoMagicalDamage(collision.GetComponent<EnemyStats>());
            collision.GetComponent<EnemyStats>().TakeDamage(100);
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
