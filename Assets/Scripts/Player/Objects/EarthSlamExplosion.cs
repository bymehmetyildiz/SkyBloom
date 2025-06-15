using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EarthSlamExplosion : MonoBehaviour
{
    
    private float groundCheckDistance;
    private LayerMask whatIsGround;
    private float coolDown;
    private float speed;
    private float timer;
    private GameObject explosionPrefab;
    private float expTimer;
    private int direciton;
    private bool canSpawnExp;
    private int expCounter = 10;


    void Start()
    {
        timer = coolDown;
    }

    public void SetupEarthSlam(float _groundCheckDistance, LayerMask _whatIsGround, float _coolDown, float _speed, GameObject _explosionPrefab, float _expTimer)
    {        
        explosionPrefab = _explosionPrefab;
        groundCheckDistance = _groundCheckDistance;
        whatIsGround = _whatIsGround;
        coolDown = _coolDown;
        speed = _speed;
        expTimer = _expTimer;
        direciton = PlayerManager.instance.player.facingDir;
        canSpawnExp = true;
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        transform.Translate(direciton * Vector2.right * Time.deltaTime * speed);

        if (timer <= 0 && gameObject != null)
            Destroy(gameObject);
    }

    private bool IsGroundDetected() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    private bool IsWallDetected() => Physics2D.Raycast(transform.position, Vector2.right * direciton, groundCheckDistance/2, whatIsGround);


    public IEnumerator CrateExplosion()
    {
        for (int i = 0; i < expCounter; i++)
        {
            if (this == null || gameObject == null)
                yield break;

            // Add a local flag before using MonoBehaviour methods
            bool groundDetected = false;
            bool wallDetected = false;

            // Try-catch to prevent errors from destroyed components
            try
            {
                groundDetected = IsGroundDetected();
                wallDetected = IsWallDetected();
            }
            catch (MissingReferenceException)
            {
                yield break; // Object destroyed
            }

            if (groundDetected && !wallDetected)
            {
                Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y - groundCheckDistance), Quaternion.identity);
                yield return new WaitForSeconds(expTimer);

                if (!canSpawnExp)
                    break;
            }
            else
            {
                yield return null; // Let next frame run
            }
        }

        if (this != null && gameObject != null)
            Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            return;
       
        if (collision.GetComponent<Enemy>() != null)
        {
            StartCoroutine(collision.GetComponent<Enemy>().ViolentKnockBack());
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.GetComponent<Entity>().SetupKnockBackDir(transform);
            enemy.TakeDamage(150);
        }
        
           

        if (collision.gameObject.name == "Ground")
        {
            canSpawnExp = false;
            Destroy(gameObject);
        }
    }

}
